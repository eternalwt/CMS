(function ($) {
	$.addbootstrapTable=function (t,p) {
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
			gridPaging: null
			,populate: function () {
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
				params[params.length]={ name: "sorname",value: p.sortname };
				params[params.length]={ name: "sortorder",value: p.sortorder };
				if(p.paging==true) {
					params[params.length]={ name: "pagesize",value: p.pagesize };
					params[params.length]={ name: "pageindex",value: p.pageindex };
				}
				$(t).addClass("loading");
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
						$(t).removeClass("loading");
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
					$(g.gridPaging).remove();
				} else {
					if(g.gridPaging!=null) {
						$(g.gridPaging).remove();
					}
					g.gridPaging=$("<div class='row'></div>");
					if(data.totalpages<=0) {
						return;
					}
					var tmpl="";
					tmpl+="<div class='span12'>";
					tmpl+="<div class='row'>";
					
					tmpl+="<div class='span1'>";
					tmpl+="<select id='rows' class='input-mini'>";
					$.each(p.rows,function (i,row) {
						tmpl+="<option value='"+row+"'>"+row+"</option>";
					});
					tmpl+="</select>";
					tmpl+="</div>";
					
					tmpl+="<div class='span5 rocord-status'>";
					tmpl+="Displaying "+(((p.pageindex-1)*p.pagesize)+1)+" to "+(p.pageindex*p.pagesize)+" of "+data.total+" items";
					tmpl+="</div>";
					
					tmpl+="<div class='span6 paging-btns'>";
				
					var disabled="";
					if(p.pageindex==1) { disabled=" disabled "; }
					tmpl+="<button class='btn' "+disabled+" index='1' id='first'>First</button>";
					disabled="";
					if(p.pageindex==1) { disabled=" disabled "; }
					tmpl+="<button class='btn' "+disabled+" index='"+(p.pageindex-1)+"' id='previous'>Previous</button>";
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
						if(i==p.pageindex) { selectClass=" btn-primary'"; }
						tmpl+="<button class='btn "+selectClass+"' index='"+i+"'>"+i+"</button></li>";
					}
					disabled="";
					if(p.pageindex==data.totalpages) { disabled=" disabled "; }
					tmpl+="<button class='btn' "+disabled+" index='"+(p.pageindex+1)+"' id='next'>Next</button>";
					disabled="";
					if(p.pageindex==data.totalpages) { disabled=" disabled "; }
					tmpl+="<button class='btn' "+disabled+" index='"+(data.totalpages)+"' id='last'>Last</button>";
					 

					tmpl+="</div>";

					tmpl+="</div>";
					tmpl+="</div>";

					g.gridPaging.append(tmpl);
					$(t).after(g.gridPaging);
					$("#rows",g.gridPaging)
					.val(p.pagesize)
					.change(function () {
						p.pagesize=this.value;
						g.populate();
					});
						$(".btn",g.pagingCtl)
					.click(function () {
						if($(this).hasClass("btn-primary")) {
							return;
						}
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
		$("[sortname!='']",t).each(function () {
			var sortName=$(this).attr("sortname");
			if($.trim(sortName)!="") {
				$(this)
				.css("cursor","pointer")
				.click(function () {
					p.sortname=$(this).attr("sortname");
					if(p.sortorder=="Asending") {
						p.sortorder="Decending";
					} else {
						p.sortorder="Asending";
					}
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
	$.fn.bootstrapTable=function (p) {
		return this.each(function () {
			if(!docloaded) {
				//$(this).hide();
				var t=this;
				$(document).ready
					(
						function () {
							$.addbootstrapTable(t,p);
						}
					);
			} else {
				$.addbootstrapTable(this,p);
			}
		});
	};
	$.fn.bootstrapTableExist=function (p) {
		return this.each(function () {
			if(this.grid&&this.p.url) {
				return true;
			}
		});
	};
	$.fn.bootstrapTableReload=function (p) {
		return this.each(function () {
			if(this.grid&&this.p.url) {
				this.grid.populate();
			}
		});
	};
	$.fn.bootstrapTableOptions=function (p) {
		return this.each(function () {
			if(this.grid) { $.extend(this.p,p); }
		});
	};
})(jQuery);