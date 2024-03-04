'use strict';

var ISAdmin = {
    BuildActionUrl: function (controller, action, id, parentId) {
        var _url = getApplicationRoot();

        if (typeof controller === 'string' && controller.length > 0) {
            _url += controller
        }

        if (typeof action === 'string' && action.length > 0) {
            _url += '/' + action
        }

        if (typeof id === 'string' || typeof id === 'number') {
            _url += '/' + id;
        }

        if (typeof parentId === 'string' || typeof parentId === 'number' || typeof parentId === 'boolean') {
            _url += '/' + parentId;
        }
        return "/" + _url;
    },
   
    ajaxGET: function (url, modal, gridId) {
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
           },
            success: function (result) {
                ISAdmin.GETResponse(result, modal, gridId);
            }
        });

    },
    GETResponse: function (result, modal, gridId) {
        if (ISAdmin.IsJSON(result))
        {
            result = ISAdmin.ParseJSON(result);          
        }
        else
        {
            if (modal != null)
            {
                $(modal).html(result);
            }
        }
        if (gridId) {
            var table = $("#" + gridId).dxDataGrid("instance");
            if (ISAdmin.isNotEmpty(table)) {
                $("#" + gridId).dxDataGrid("getDataSource").reload();
            }
        }
    },

    ajaxGET_Status: function (url, gridId, modal) {
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
            },
            success: function (result) {
                ISAdmin.GETResponse_Status(result, gridId, modal);
            } 
        });

    },
    GETResponse_Status: function (result, gridId, modal) {
        if (ISAdmin.IsJSON(result)) {
            result = ISAdmin.ParseJSON(result);
            if (modal == "#lgModalRolesBody") {
                ISAdmin.HideRolesModals();
            }
            else {
                ISAdmin.HideModals();
            }
            if (gridId) {
                var table = $("#" + gridId).dxDataGrid("instance");
                if (ISAdmin.isNotEmpty(table)) {
                    $("#" + gridId).dxDataGrid("getDataSource").reload();
                }
            }
        }
    },
    ajaxPOST: function (url, form, modal, gridId) {
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: form.serialize(),
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
            },
            success: function (result) {
                ISAdmin.POSTResponse(result, modal, gridId);
            }
        });
    },
    POSTResponse: function (result, modal, gridId)
    {
        if (ISAdmin.IsJSON(result))
        {
            result = ISAdmin.ParseJSON(result);
            if (modal == "#lgModalRolesBody")
            {
                    ISAdmin.HideRolesModals();
            }
            else
            {
                    ISAdmin.HideModals();
            }
            if (gridId)
            {
                $("#" + gridId).dxDataGrid("getDataSource").reload();
            }
        }
        else if (!ISAdmin.IsJSON(result)) {
            if (modal != null) {
                $(modal).html(result);
            }
        }
    },
   
    DrawPartialView: function (url, divId, popstate) {
        var $popstate = popstate;
        history.replaceState('', null, '');
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                },
            },
            success: function (result, e) {
                if (divId == null) {
                    $('#bodyContent').html(result);
                    if (!$popstate) {
                        pushHistory(url);
                    }
                }
                else {
                    $('#' + divId).html(result);
                }

            },
        });
    },
    DrawPartialModal: function (url, modalId) {

        if (modalId == "xlModalBody") {
            ISAdmin.ExtraLargeModal();
        }
        else if (modalId == "lgModalBody") {
            modalId
            ISAdmin.LargeModal();
        }
        else if (modalId == "lgModalRolesBody") {
            modalId
            ISAdmin.LargeModalRoles();
        }

        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            beforeSend: function () {

            },
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
            },
            success: function (result) {
                if (!ISAdmin.IsJSON(result)) {
                    $('#' + modalId).html(result);
                }
                else {

                }

            }
        });
    },
   
    IsJSON: function (value) {
        try {
            JSON.parse(value);
        } catch (e) {
            return false;
        }
        return true;
    },
    ParseJSON: function (value) {
        try {
            var parsed = JSON.parse(value);
            return parsed;
        } catch (e) {

            var json = {
                parsed: value,
                e: e,
            };
            return json;
        }
        return value;
    },
   
    LargeModal: function () {
        var myModal = new bootstrap.Modal(document.getElementById('lgModal'), {
            keyboard: false
        });
        myModal.toggle();
        myModal.show();
    },
    LargeModalRoles: function () {
        var myModal = new bootstrap.Modal(document.getElementById('lgModalRoles'), {
            keyboard: false
        });
        $('#xlModal').css("z-index", "1040");
        myModal.toggle();
        myModal.show();
    },
    ExtraLargeModal: function () {
        var myModal = new bootstrap.Modal(document.getElementById('xlModal'), {
            keyboard: false
        });
        myModal.toggle();
        myModal.show();
    },
    HideModals: function () {
        $('#lgModal,#xlModal').modal('hide');
    },
    HideRolesModals: function () {
        $('#lgModalRoles').modal('hide');
        $('#xlModal').css("overflow-x", "hidden");
        $('#xlModal').css("overflow-y", "auto");
    },
};
