function Module() {
	this.saveURL = '/Admin/Module/Create';
	this.deleteURL = '/Admin/Module/Delete';
	this.listURL = '/Admin/Module/List';
	this.findURL = '/Admin/Module/Find';
	this.onSave = null;
	this.onDelete = null;
	this.onEditInfo = null;
	this.onList = null;
	this.onError = null;
	this.Load = function (id) {
		var onsuccess = this.onEditInfo;
		var onerror = this.onError;
		$.ajax({
			type: "GET",
			url: this.findURL + "/" + id,
			dataType: "JSON",
			cache: false,
			success: function (data) { if (onsuccess) { onsuccess(data); } },
			error: function (data) { if (onerror) { onerror(data); } }
		});
	}
	this.Save = function (params) {
		var onsuccess = this.onSave;
		var onerror = this.onError;
		$.ajax({
			type: "POST",
			url: this.saveURL,
			data: params,
			dataType: "JSON",
			cache: false,
			success: function (data) { if (onsuccess) { onsuccess(data); } },
			error: function (data) { if (onerror) { onerror(data); } }
		});
	};
	this.Delete = function (id) {
		var onsuccess = this.onDelete;
		var onerror = this.onError;
		$.ajax({
			type: "GET",
			url: this.deleteURL + "/" + id,
			dataType: "JSON",
			cache: false,
			success: function (data) { if (onsuccess) { onsuccess(data); } },
			error: function (data) { if (onerror) { onerror(data); } }
		});
	}
}
