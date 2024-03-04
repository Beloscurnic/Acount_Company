var companyIdGrid;

$("#gridContainer").dxDataGrid({
    showBorders: true,
    dataSource: DevExpress.data.AspNet.createStore({
        key: "ID",
        loadUrl: "Get_Company_List",
        onBeforeSend(method, ajaxOptions) {
            ajaxOptions.xhrFields = { withCredentials: true };
        },
    }),
    columns: [
        { dataField: "ID", width: 70 },
        { dataField: "JuridicalName" },
        { dataField: "CommercialName" },
        { dataField: "Email" },
        {
            caption: "Status",
            calculateCellValue: function (data) { return getStatusText(data.Status); },
            allowEditing: true
        },
        {
            name: "contextBtns",
            type: "buttons",
            buttons: [
                { name: "edit", icon: "edit", onClick: editButtonClicked, visible: true },
                { name: "Context", icon: "overflow", onClick: onClickContextMenuBtn }
            ]
        }
    ],
    toolbar: {
        items: [
            {
                location: "before",
                widget: "dxButton",
                options: {
                    text: "Add",
                    icon: "plus",
                    stylingMode: "contained",
                    type: "success",
                    onClick: addCompanyButtonClicked,
                    visible: true
                }
            },
            {
                location: "after",
                locateInMenu: "auto",
                widget: "dxButton",
                options: {
                    text: "Refresh",
                    icon: "refresh",
                    stylingMode: "contained",
                    type: "success",
                    onClick: getCompanyList,
                    visible: true
                }
            }
        ]
    }
});

// Создание контекстного меню для NewRegisteredContext
$("#NewRegisteredContext").dxContextMenu({
    target: 'null',
    onItemClick: onContextMenuItemClick,
    position: { at: "left bottom" },
    dataSource: [
        { id: "disable", text: "Activate" },
        { id: "active", text: "Disable" }
    ]
});

// Создание контекстного меню для ActiveContext
$("#ActiveContext").dxContextMenu({
    target: 'null',
    onItemClick: onContextMenuItemClick,
    position: { at: "left bottom" },
    dataSource: [
        { id: "active", text: "Disable" }
    ]
});

// Создание контекстного меню для DisabledContext
$("#DisabledContext").dxContextMenu({
    target: 'null',
    onItemClick: onContextMenuItemClick,
    position: { at: "left bottom" },
    dataSource: [
        { id: "disable", text: "Activate" }
    ]
});
function addCompanyButtonClicked(e) {
    ISAdmin.DrawPartialModal(ISAdmin.BuildActionUrl("Company", "CompanyID", 0), "xlModalBody");
}

function editButtonClicked(e) {
    e.component.selectRowsByIndexes(e.row.rowIndex);
    ISAdmin.ExtraLargeModal();
    var id = e.row.data.ID;
    var modal = '#xlModalBody';
    ISAdmin.ajaxGET(ISAdmin.BuildActionUrl("Company", "CompanyID", id), modal);
}
function getCompanyList() {
    return $("#gridContainer").dxDataGrid("instance").refresh(true);
}

function onClickContextMenuBtn(e) {
    e.component.selectRowsByIndexes(e.row.rowIndex);

    var status = e.row.data.Status;
    companyIdGrid = e.row.data.ID;
    if (status == 0 || status == 'NewRegistered') {
        var contextMenu = $("#NewRegisteredContext").dxContextMenu('instance');
        contextMenu.option('target', e.event.target)
        $("#NewRegisteredContext").dxContextMenu("show");
    }
    else if (status == 1 || status == 'Active') {
        var contextMenu = $("#ActiveContext").dxContextMenu('instance');
        contextMenu.option('target', e.event.target)
        $("#ActiveContext").dxContextMenu("show");
    }
    else if (status == 2 || status == 'Disabled') {
        var contextMenu = $("#DisabledContext").dxContextMenu('instance');
        contextMenu.option('target', e.event.target)
        $("#DisabledContext").dxContextMenu("show");
    }
}

function onContextMenuItemClick(e) {
    switch (e.itemData.id) {
        case 'disable': {
            changeStatusButtonClicked(e, "Disabled");
            break;
        }
        case 'active': {
            changeStatusButtonClicked(e, "Activated");
            break;
        }
    }
    $("#gridContainer").dxDataGrid("getDataSource").reload();
    /*$("#gridContainer").dxDataGrid("instance").refresh(true);*/
}
function changeStatusButtonClicked(e, status) {
    ISAdmin.ajaxGET_Status("/Company/ChangeStatusCompany?id=" + companyIdGrid + "&status=" + status);
}

function getStatusText(status) {
    switch (status) {
        case 0:
            return "NewRegistered";
        case 1:
            return "Activated";
        case 2:
            return "Disable";
        default:
            return "";
    }
}
