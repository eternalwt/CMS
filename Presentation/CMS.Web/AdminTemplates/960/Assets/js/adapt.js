(function (w,d,config,undefined) {
	if(!config) {
		return;
	}
	var url,timer;
	var callback=typeof config.callback==='function'?config.callback:undefined;
	var path=config.path?config.path:'';
	var range=config.range;
	var range_len=range.length;
	var oldurls=new Array();
	function change(i,width) {
		var css=d.createElement('link');
		css.rel='stylesheet';
		css.media='screen';
		css.href=url;
		if(checkoldurl(url)==false) {
			oldurls[oldurls.length]=url;
		}
		path&&(d.head||d.getElementsByTagName('head')[0]).appendChild(css);
		callback&&callback(i,width);
	}
	function adapt() {
		clearTimeout(timer);
		var width=w.innerWidth||d.documentElement.clientWidth||d.body.clientWidth||0;
		var arr,arr_0,val_1,val_2,is_range,file;
		var i=range_len;
		var last=range_len-1;
		var isChange=false;
		while(i--) {
			url='';
			arr=range[i].split('=');
			arr_0=arr[0];
			file=arr[1]?arr[1].replace(/\s/g,''):undefined;
			is_range=arr_0.match('to');
			val_1=is_range?parseInt(arr_0.split('to')[0],10):parseInt(arr_0,10);
			val_2=is_range?parseInt(arr_0.split('to')[1],10):undefined;
			if((!val_2&&i===last&&width>val_1)||(width>val_1&&width<=val_2)) {
				if(isChange==false&&oldurls.length>1) {
					oldurls.length=null;
					isChange=true;
				}
				file&&(url=path+file);
				if(oldurls.length==0) {
					change(i,width);
				}
				else if(checkoldurl(url)==false) {
					change(i,width);
				}
			}
		}
	}
	adapt();
	function checkoldurl(url) {
		var i=0;
		var isExist=false;
		for(i=0;i<oldurls.length;i++) {
			if(oldurls[i]==url) {
				isExist=true;
				break;
			}
		}
		return isExist;
	}
	function react() {
		clearTimeout(timer);
		timer=setTimeout(adapt,16);
	}
	if(config.dynamic) {
		if(w.addEventListener) {
			w.addEventListener('resize',react,false);
		}
		else if(w.attachEvent) {
			w.attachEvent('onresize',react);
		}
		else {
			w.onresize=react;
		}
	}
})(this,this.document,ADAPT_CONFIG);