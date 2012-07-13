$(document).ready(function () {
	MODULE.init();
});
var MODULE={
	init: function () {
	}
	,open: function (id) {
	}
	,onSuccess: function (grid,data) {
		$("#SelectAll",grid).click(function () {
			if(this.checked) {
				$(":input[id='ModuleID']",grid).attr("checked","checked");
			} else {
				$(":input[id='ModuleID']",grid).removeAttr("checked");
			}
		});
	}
	,onError: function (data) {
		alert(data);
	}
}