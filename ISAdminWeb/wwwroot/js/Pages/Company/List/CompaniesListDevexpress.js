
var companyIdGrid;
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

function infoButtonClicked(e) {
    e.component.selectRowsByIndexes(e.row.rowIndex);
    ISAdmin.ExtraLargeModal();
    var id = e.row.data.ID;
    var modal = '#xlModalBody';
    ISAdmin.ajaxGET(ISAdmin.BuildActionUrl("Company", "GetCompanyInfo", id), modal);
}

function getCompanyList() {
	return $("#gridContainer").dxDataGrid("instance").refresh(true);
}


//$('#ActiveContext').dxContextMenu({
//	target: 'null',
//	onItemClick(e) {
//		onContextMenuItemClick(e);
//	},
//	position: {
//		at: ['Left', 'Bottom'],
//	},
//	dataSource: [{
//		id: 'manage', text: formatMessage('Manage')
//	},
//	{
//		id: 'disable', text: formatMessage('Disable')
//	}]
//}).dxContextMenu('instance');

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
            changeStatusButtonClicked(e,"Disabled" );
            break;
        }
        case 'active': {
            changeStatusButtonClicked(e,"Active");
            break;
        }
    }
}
function changeStatusButtonClicked(e, status) {
    ISAdmin.ExtraLargeModal();
    var id = companyIdGrid;
    var modal = '#xlModalBody';
    var url = "/Company/ChangeStatusCompanyGet?id=" + encodeURIComponent(id) + "&status=" + encodeURIComponent(status);
    ISAdmin.ajaxGET(url, modal);
}

//function changeStatusButtonClickedPOST(e, status) {
//    ISAdmin.ExtraLargeModal();
//    var rowId = companyIdGrid;
//    var modal = '#xlModalBody';
//    ISAdmin.ajaxGET_Status("/Company/ChangeStatusCompany?id=" + companyIdGrid + "&status=" + status, rowId, modal);	
//}