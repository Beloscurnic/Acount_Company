'use strict'

function onSubmitClick(e) {
	e.event.preventDefault();
	var gridId = "gridContainer";
	var $form = $('form[name="upsertCompany"]');
	var url = $form.attr('action');
	var modal = '#xlModalBody';

	ISAdmin.ajaxPOST(url, $form, modal, gridId);
}

function editButtonClicked(e) {
	e.component.selectRowsByIndexes(e.row.rowIndex);

	ISAdmin.ExtraLargeModal();
	const data = e.row.data;

	if (data) {
		var id = data.ID;
		var modal = '#xlModalBody';
		ISAdmin.ajaxGET(ISAdmin.BuildActionUrl("Company", "CompanyID", id), modal);
	}

}



function onClickActivateCompany(e) {
	e.event.preventDefault();
    var gridId = "gridContainer";
    var id = $("input[name='ID']").val()
    var url = "/Company/ChangeStatusCompany?id=" + encodeURIComponent(id) + "&status=" + "Activated";

	changeStatus(url, gridId);

}

function onClickDeactivateCompany(e, status) {
    e.event.preventDefault();
    var gridId = "gridContainer";
    var id = $("input[name='ID']").val()
    var url = "/Company/ChangeStatusCompany?id=" + encodeURIComponent(id) + "&status=" + "Disabled";
	changeStatus(url, gridId);
}

function getStatusText(status) {
    switch (status) {
        case 0:
            return "NewRegistered";
        case 1:
            return "Activated";
        case 2:
            return "Disabled";
        default:
            return "";
    }
}

function changeStatus(url, gridId) {
    $.ajax({
        url: url,
        cache: false,
        type: "GET",
        dataType: "json",
        statusCode: {
            302: function (data) {
                window.location.href = '/Account/Logout/';
            }
        },
        success: function (result) {
                ISAdmin.HideModals();
                $("#" + gridId).dxDataGrid("getDataSource").reload();        
        }
    });
}