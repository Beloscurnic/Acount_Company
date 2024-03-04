
$.ajax({
    url: "/Base/GetUserClaims/",
    cache: false,
    type: "GET",
    dataType: "json",
    statusCode: {
        302: function (data) {
            window.location.href = '/Account/Logout/';
        }
    },
    success: function (result) {
        var languageUser = result.UiLanguage.toLowerCase() != null ? result.UiLanguage.toLowerCase() : "ru";
        DevExpress.localization.locale(languageUser);
    }
});
var formatMessage = DevExpress.localization.formatMessage;

var lgModal = document.getElementById('lgModal');
var xlModal = document.getElementById('xlModal');
var lgRolesModal = document.getElementById('lgModalRoles');
var html = '<div id="spinner-content" class="d-flex justify-content-center">' +
    '<div class="" name="Growing-Spinners">' +
    '<div class="spinner-grow text-primary" role = "status" style="height:3rem;width:3rem;" >' +
    '<span class="sr-only">Loading...</span>' +
    '</div >' +
    '<div class="spinner-grow text-secondary" role="status" style="height:3rem;width:3rem;">' +
    ' <span class="sr-only">Loading...</span>' +
    '</div>' +
    '<div class="spinner-grow text-success" role="status" style="height:3rem;width:3rem;">' +
    '<span class="sr-only">Loading...</span>' +
    '</div>' +
    '<div class="spinner-grow text-danger" role="status"style="height:3rem;width:3rem;">' +
    '<span class="sr-only">Loading...</span>' +
    '</div>' +
    '<div class="spinner-grow text-warning" role="status" style="height:3rem;width:3rem;">' +
    '<span class="sr-only">Loading...</span>' +
    '</div>' +
    '<div class="spinner-grow text-info" role="status" style="height:3rem;width:3rem;">' +
    '<span class="sr-only">Loading...</span>' +
    '</div>' +
    '</div >' +
    '</div >';

//destroy all chidren of modal
lgModal.addEventListener('hidden.bs.modal', function () {
    $(this).removeData();
    $("#lgModalBody").html(html);

})

xlModal.addEventListener('hidden.bs.modal', function () {
    $(this).removeData();
    $("#xlModalBody").html(html);
})

lgRolesModal.addEventListener('hidden.bs.modal', function () {

    $('#xlModal').css("z-index", "1050");
    $(this).removeData();
    $("#lgModalRolesBody").html(html);


})
////clear inputs in toasts
//$('.toast').on('hidden.bs.toast', function () {
//    $('label').html('');
//})

var ISBreadcrumb = {
    Breadcrumb: function (breadcrumbData) {

        var breadcrumbContainer = document.getElementById("breadcrumb");

        // Clear existing breadcrumb
        breadcrumbContainer.innerHTML = "";

        // Create breadcrumb links
        for (var i = 0; i < breadcrumbData.length; i++) {
            var list = document.createElement("li");
            if (i !== breadcrumbData.length - 1) {
                list.className = "breadcrumb-item";

                breadcrumbContainer.appendChild(list);

                var link = document.createElement("a");
                link.href = breadcrumbData[i].url;
                link.setAttribute('onclick', breadcrumbData[i].onclick);
                link.textContent = breadcrumbData[i].title;

                list.appendChild(link);
            }
            else {
                list.className = "breadcrumb-item active";

                breadcrumbContainer.appendChild(list);

                var link = document.createElement("a");
                link.textContent = breadcrumbData[i].title;

                list.appendChild(link);
            }
        }
    }
}

function changeTitle(newTitle) {
    $('title').text(newTitle);
}

function ShowToast(id, text) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr[id](text);
}

function showDangerAlert(message) {
    toastr["danger"](message);
}

function closeModalClick(e) {
    ISAdmin.HideModals();
}

function closeRolesModalClick(e) {
    ISAdmin.HideRolesModals();
}

function manageCompany(companyIdGrid) {
    $.ajax({
        url: '/Company/ManageCompany/' + companyIdGrid,
        cache: false,
        type: "GET",
        dataType: "json",
        statusCode: {
            302: function (data) {
                window.location.href = '/Account/Logout/';
            }
        },
        success: function (result, e) {

            if (result.Result == 1) {

                var tokenString = result.Record.Token;
                var token = Math.ceil(tokenString.length / 3);
                var part1 = tokenString.slice(0, token);
                var part2 = tokenString.slice(token, token * 2);
                var part3 = tokenString.slice(token * 2);
                var url = result.Record.URL + part1 + '/' + part2 + '/' + part3 + '/';
                window.open(url, '_blank');
            }
            else if (result.Result == 2) {
                ShowToast('warning', result.Message);
            }
            else if (result.Result == 3) {
                ShowToast('error', result.Message);
            }
            else if (result.Result == 5) {
                ShowToast('error', result.Message);
            }
            else {
                ShowToast('error', "Something went wrong!");
            }

        }
    });
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
            if (result.Result == 1) {
                ISAdmin.HideModals();
                ShowToast('success', result.Message);

                $("#" + gridId).dxDataGrid("getDataSource").reload();
            }
            else if (result.Result == 2) {
                ShowToast('warning', result.Message);
            }
            else if (result.Result == 5) {
                ShowToast('error', result.Message);

            }

        }
    });
}

function changeStatusRoles(url, gridId) {
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
            if (result.Result == 1) {
                ISAdmin.HideRolesModals();
                ShowToast('success', result.Message);

                $("#" + gridId).dxDataGrid("getDataSource").reload();
            }
            else if (result.Result == 2) {
                ShowToast('warning', result.Message);
            }
            else if (result.Result == 3) {
                ShowToast('error', result.Message);

            }
        }
    });
}

function getProgressBarInstance() {
    return $("#upload-progress").dxProgressBar("instance");
}

function fileUploader_dropZoneEnter(e) {
    if (e.dropZoneElement.id === "dropzone-external")
        toggleDropZoneActive(e.dropZoneElement, true);
}

function fileUploader_dropZoneLeave(e) {
    if (e.dropZoneElement.id === "dropzone-external")
        toggleDropZoneActive(e.dropZoneElement, false);
}

function fileUploader_uploaded(e) {
    const file = e.value[0];
    const fileReader = new FileReader();
    fileReader.onload = function () {
        toggleDropZoneActive($("#dropzone-external")[0], false);
        $("#dropzone-image")[0].src = fileReader.result;
    }
    fileReader.readAsDataURL(file);
    $("#dropzone-text")[0].style.display = "none";
    getProgressBarInstance().option({
        visible: false,
        value: 0
    });
}

function fileUploader_progress(e) {
    getProgressBarInstance().option("value", e.bytesLoaded / e.bytesTotal * 100);
}

function fileUploader_uploadStarted() {
    toggleImageVisible(false);
    getProgressBarInstance().option("visible", true);
}

function toggleDropZoneActive(dropZone, isActive) {
    if (isActive) {
        dropZone.classList.add("dx-theme-accent-as-border-color");
        dropZone.classList.remove("dx-theme-border-color");
        dropZone.classList.add("dropzone-active");
    } else {
        dropZone.classList.remove("dx-theme-accent-as-border-color");
        dropZone.classList.add("dx-theme-border-color");
        dropZone.classList.remove("dropzone-active");
    }
}

function toggleImageVisible(visible) {
    $("#dropzone-image")[0].hidden = !visible;
}

function onValueChanged(e) {
    fileUploader_uploadStarted();
    fileUploader_progress(e);
    encodeImageFileAsURL(e);
    fileUploader_uploaded(e);
}

function encodeImageFileAsURL(e) {
    //code for input in stirng
    var file = e.value[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        //console.log('RESULT', reader.result)
        $("#" + e.element[0].id + "String").dxTextBox("instance").option("value", reader.result);
    }
    reader.readAsDataURL(file);
}

function closeTicket(url) {
    var id = $("#TicketOID").dxTextBox("instance").option().value;

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
            if (result.Result == 1) {
                ShowToast('success', result.Message);

            }
            else if (result.Result == 2) {
                ShowToast('warning', result.Message);
            }
            else if (result.Result == 3) {
                ShowToast('error', result.Message);

            }
            window.location = ISAdmin.BuildActionUrl("BugTracker", "Ticket", id);
        }
    });
}

function GetNavigations() {
    var navs = {
        isAdmin: $('#isAdmin').val(),
        navigationsRaw: $('#navigations').val(),
    }
    return navs;
}

function UserRoles() {
    var navs = GetNavigations();
    var isAdmin = navs.isAdmin.toLowerCase() == "true" ? true : false;
        $('#sidebar-menu').removeAttr('hidden');
        $('#side-menu').children().show();
        $('#ManagementMenu').children().show();
        $('#CatalogMenu').children().show();
        $('#CloudOrchestratorMenu').children().show();
        $('#ClientMapMenu').children().show();
        $('#AccountingMenu').children().show();
        $('#SupportCenterMenu').children().show();
        $('#EventViewerMenu').children().show();
        $('#EmailNotificationsMenu').children().show();
    
}

function buttonVisible(e) {
    var navs = GetNavigations();
    var isAdmin = navs.isAdmin.toLowerCase() == "true" ? true : false;
    if (!isAdmin) {
        var navigationsRaw = navs.navigationsRaw;
        if (ISAdmin.isNotEmpty(navigationsRaw)) {
            if (navigationsRaw.includes('{"Name":"' + e + '","PermissionState":1}')) {
                return false;
            }
            else if (navigationsRaw.includes('{"Name":"' + e + '","PermissionState":2}')) {
                return true;
            }
        }
        else {
            return false;
        }
    }
    else {
        return true;
    }
    return true;

}


function inputDisable() {

}

function pushHistory(url) {
    //if (window.history && window.history.pushState) {
    //	window.history.pushState('', null, url);
    //	window.onpopstate = (event) => {
    //		if (document.location != '') {
    //			ISAdmin.DrawPartialView(document.location, null, true);
    //		}
    //		else {
    //			window.location.href = "/Home/Index"
    //			//ISAdmin.DrawPartialView("/Home/Index");
    //		}
    //	};
    //}
}