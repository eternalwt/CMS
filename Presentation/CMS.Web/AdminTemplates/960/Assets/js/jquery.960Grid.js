(function ($) {
	$.addGrid960=function (t,p) {
		if(t.grid) { return false; }
		p=$.extend({
			url: ""
			,method: 'POST'
			,autoload: true
			,sortname: ''
			,sortorder: 'Asending'
			,onSuccess: false
			,onSubmit: false
			,onError: false
			,params: null
			,gridLength: 24
			,pagesize: 10
			,pageindex: 1
			,paging: false
			,rows: [10,20,30,40,50,100,200]
		},p);
		var g={
			populate: function () {
				if(p.onSubmit) {
					var gh=p.onSubmit(p);
					if(!gh) { return false; }
				}
				var params=new Array();
				if(p.params!=null) {
					$.each(p.params,function (i,param) {
						params[params.length]=param;
					});
				}
				params[params.length]={ name: "SortName",value: p.sortname };
				params[params.length]={ name: "SortOrder",value: p.sortorder };
				if(p.paging==true) {
					params[params.length]={ name: "PageSize",value: p.pagesize };
					params[params.length]={ name: "PageIndex",value: p.pageindex };
				}
				$.ajax({
					type: p.method,
					url: p.url,
					data: params,
					dataType: "JSON",
					cache: false,
					success: function (data) {
						if(p.onSuccess) {
							p.onSuccess(t,data);
						}
						g.buildpager(data);
					},
					error: function (data) {
						if(p.onError) {
							p.onError(data);
						}
					}
				});
			}
			,buildpager: function (data) {
				if(p.paging==false) {
					$(".grid-paging",t).remove();
				} else {
					var gridpaging=$("<div class='grid_"+p.gridLength+" grid-paging'></div>");
					var tmpl="";
					tmpl+="<div class='grid_"+(p.gridLength/4)+" alpha rows'>";
					tmpl+="<select id='rows'>";
					$.each(p.rows,function (i,row) {
						tmpl+="<option value='"+row+"'>"+row+"</option>";
					});
					tmpl+="</select>";
					tmpl+="</div>";
					tmpl+="<div class='grid_"+(p.gridLength/4)+" disp-rows'>";
					tmpl+="Displaying "+(((p.pageindex-1)*p.pagesize)+1)+" to "+(p.pageindex*p.pagesize)+" of "+data.total+" items";
					tmpl+="</div>";
					tmpl+="<div class='grid_"+(p.gridLength/2)+" omega paging-buttons' style='text-align:right'>";
					var disabled="";
					if(p.pageindex==1) { disabled="disabled"; }
					tmpl+="<button index='1' id='first' class='btn "+disabled+"'>First</button>";
					disabled="";
					if(p.pageindex==1) { disabled="disabled"; }
					tmpl+="<button index='"+(p.pageindex-1)+"' id='previous' class='btn "+disabled+"'>Previous</button>";

					var iPageCount=5;
					var iPageCountHalf=Math.floor(iPageCount/2);
					var iPages=data.totalpages;
					var iCurrentPage=p.pageindex;
					var iStartButton,iEndButton,i,iLen;
					/* Pages calculation */
					if(iPages<iPageCount) {
						iStartButton=1;
						iEndButton=iPages;
					}
					else if(iCurrentPage<=iPageCountHalf) {
						iStartButton=1;
						iEndButton=iPageCount;
					}
					else if(iCurrentPage>=(iPages-iPageCountHalf)) {
						iStartButton=iPages-iPageCount+1;
						iEndButton=iPages;
					}
					else {
						iStartButton=iCurrentPage-Math.ceil(iPageCount/2)+1;
						iEndButton=iStartButton+iPageCount-1;
					}
					/* Build the dynamic list */
					var count=0;
					for(i=iStartButton;i<=iEndButton;i++) {
						var selectClass="";
						if(i==p.pageindex) { selectClass=" selected "; }
						tmpl+="<button href='#' index='"+i+"' class='btn "+selectClass+"'>"+i+"</button>";
					}
					disabled="";
					if(p.pageindex==data.totalpages) { disabled="disabled"; }
					tmpl+="<button index='"+(p.pageindex+1)+"' id='next' class='btn "+disabled+"'>Next</button>";
					disabled="";
					if(p.pageindex==data.totalpages) { disabled="disabled"; }
					tmpl+="<button index='"+(data.totalpages)+"' id='last' class='btn "+disabled+"'>Last</button>";
					tmpl+="</div>";
					gridpaging.append(tmpl);
					$(".grid-paging",t).remove();
					$(".grid-content",t).after(gridpaging);
					$("#rows",gridpaging)
					.val(p.pagesize)
					.change(function () {
						p.pagesize=this.value;
						g.populate();
					});
					$(".btn:not(.select):not(.disabled)",gridpaging)
					.click(function () {
						p.pageindex=parseInt($(this).attr("index"));
						if(isNaN(p.pageindex)) { p.pageindex=0; }
						if(p.pageindex>0) {
							g.populate();
						}
					})
					;
				}
			}
		};
		$(t).addClass("grid-960");
		$("div[sortname!='']",t).each(function () {
			var sortName=$(this).attr("sortname");
			if($.trim(sortName)!="") {
				$(this).click(function () {
					p.sortname=$(this).attr("sortname");
					var sortorder="";
					if(p.sortorder=="Asending") {
						p.sortorder="Decending";
						sortorder="desc";
					} else {
						p.sortorder="Asending";
						sortorder="asc";
					}
					$(".sort-column",t).removeClass("sort-column");
					$(".asc",t).removeClass("asc");
					$(".desc",t).removeClass("desc");
					$(this).addClass("sort-column ").addClass(sortorder);
					g.populate();
				});
			}
		});
		if(p.autoload==true) {
			g.populate();
		}
		t.grid=g;
		return t;
	};

	var docloaded=false;
	$(document).ready(function () { docloaded=true });
	$.fn.grid960=function (p) {
		return this.each(function () {
			if(!docloaded) {
				//$(this).hide();
				var t=this;
				$(document).ready
					(
						function () {
							$.addGrid960(t,p);
						}
					);
			} else {
				$.addGrid960(this,p);
			}
		});
	};
	$.fn.grid960Exist=function (p) {
		return this.each(function () {
			if(this.grid&&this.p.url) {
				return true;
			}
		});
	};
	$.fn.grid960Reload=function (p) {
		return this.each(function () {
			if(this.grid&&this.p.url) {
				this.grid.populate();
			}
		});
	};
	$.fn.grid960Options=function (p) {
		return this.each(function () {
			if(this.grid) { $.extend(this.p,p); }
		});
	};
})(jQuery);