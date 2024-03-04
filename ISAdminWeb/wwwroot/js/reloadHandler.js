//window.onkeydown = function (event) {

//    if (event.which === 116 || (event.which === 82 && event.ctrlKey)) {
//        event.preventDefault();

//        var drawUrl = window.location.pathname;
//        if (drawUrl == "/Account/LockScreen") {
//            window.location.href = '/Account/LockScreen';
//        }
//        else {
//            if (drawUrl == "/") {
//                window.location.reload();
//            }
//            else {
//                var replacedText = drawUrl.replace(/\//g, "_");
//                window.location.href = "/Redirect/Home/Home/" + replacedText;
//            }
//        }
//    }
//};


//window.onbeforeunload = function (e) {
//	return 'Dialog text here.';
//};

//window.onbeforeunload = function (e) {
//	e.preventDefault();
//	if (performance.navigation.type === performance.navigation.TYPE_RELOAD) {
//		console.log("Reload button clicked");
//	}
//	return "text";
//};

var time = new Date().getTime();
var page = "";
$(document.body).bind("mousemove keypress", function (e) {
	page = e.target.ownerDocument.location.pathname;

	time = new Date().getTime();
});

function refresh() {
	if (page == "/Account/Login") {
		return;
	}
	else {
		var timeNow = new Date().getTime();
		if (timeNow - time >= 1200000)
			window.location.href = '/Account/LockScreen/';
		else
			setTimeout(refresh, 60000);
	}
}

setTimeout(refresh, 60000);