

var ISAdminDevextreme = {
    ajaxCustomStoreGET: function (url) {
        var store = new DevExpress.data.CustomStore({
            key: "ID",
            loadMode: "raw",
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
            },
            load: function (loadOptions) {
                var d = $.Deferred();
                ISAdminDevextreme.ajaxJsonGET(url, d);

                return d.promise();
            }
        });

        return store;
    },
    ajaxCustomStoreIdGET: function (url) {
        var store = new DevExpress.data.CustomStore({
            key: "Id",
            loadMode: "raw",
            statusCode: {
                302: function (data) {
                    window.location.href = '/Account/Logout/';
                }
            },
            load: function (loadOptions) {
                var d = $.Deferred();
                ISAdminDevextreme.ajaxJsonGET(url, d);

                return d.promise();
            }
        });

        return store;
    },
   
    ajaxJsonGET: function (url, d) {
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
                d.resolve(result);
            }
        });
    },
 
}
