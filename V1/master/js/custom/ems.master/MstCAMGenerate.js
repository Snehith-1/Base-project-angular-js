(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCAMGenerateController', MstCAMGenerateController);

    MstCAMGenerateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function MstCAMGenerateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {

        $scope.title = 'MstCAMGenerateController';
        var vm = this;
        var froalaConfigKey = apiManage.GetCommonData['froalaConfig'].Key;
        activate();
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        if (localStorage.getItem('RefreshTemplate') && localStorage.getItem('RefreshTemplate') != 'Y') {
            location.reload();
            localStorage.setItem('RefreshTemplate', 'Y');
            return false;
        }
        function activate() {
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = true;
            // var url = "api/MstCAMGeneration/GetCAMTemplate"
            // lockUI();
            var param = {
                application_gid: $location.search().application_gid
            };
            // SocketService.getparams(url, param).then(function (resp) {
            //     unlockUI();
            //     $scope.content = resp.data.template_content;
            //     document.getElementById('test').innerHTML += $scope.content;

            // }); 
            var url = "api/MstCAMGeneration/GetApp2CAM"
            lockUI();
            SocketService.getparams(url, param).then(function (resp) { 
                if(resp.data.status==true){
                    $scope.showmstcontent = resp.data.mstcontent_flag;
                    if(resp.data.mstcontent_flag=="Y"){
                        unlockUI();
                        $scope.MdlTemplatelist = resp.data.MdlTemplatedtl;   
                    }
                    else{
                        unlockUI();
                        $scope.camcontent = resp.data.template_content;
                        $scope.content = resp.data.defaulttemplate_content;
                        document.getElementById('camtemplate').innerHTML += $scope.content;
                        //$scope.checkerpushback_remarks = resp.data.checkerpushback_remarks;
                        $scope.lspath = resp.data.lspath;
                        $scope.lsname = resp.data.lsname;
                        $scope.template_name = resp.data.template_name;
                        $scope.template_gid  = resp.data.template_gid;
                    } 
                }
                else{
                    unlockUI();
                }
            });
        }

        
        $scope.camdocdownload = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
         
        }
        $scope.back = function () {
            var application_gid = $scope.application_gid;

            if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }
        $scope.WordGenerate = function () {
            // if ($scope.cam_content != '' && $scope.cam_content != null) {
            //     $scope.content = $scope.cam_content;
            // }
            var previewtemplate_content = getPreviewcontent($scope.content);
            var templatewordcontent = getPreviewWordcontent($scope.content);
            var params = {
                application_gid: $location.search().application_gid,
                content: previewtemplate_content,
                defaulttemplate_content: $scope.content,
                templatewordcontent: templatewordcontent
            }
            var url = 'api/MstCAMGeneration/WordGenerate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    Notify.alert('CAM Generated Successfully !', 'success')

                    activate();
                }
                else {
                console.log(resp.data.message);
                    Notify.alert('Error Occurred While Downloading !', 'warning')
                    activate();

                }
            });
        }
        $scope.WordSave = function () {
            // if ($scope.cam_content != '' && $scope.cam_content != null) {
            //     $scope.content = $scope.cam_content;
            // }
            var previewtemplate_content = getPreviewcontent($scope.content); 
            var params = {
                application_gid: $location.search().application_gid,
                content: previewtemplate_content,
                defaulttemplate_content: $scope.content
            }
            var url = 'api/MstCAMGeneration/PostWordSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();

                }
            });
        }
        function extractContent(s) {
            var span = document.createElement('span');
            span.innerHTML = s;
            var txtx = span.innerHTML;
            var children = span.querySelectorAll('*');
            var task = {
                list: []
            }
            for (var i = 0; i < children.length; i++) {
                var getinputdata = "";
                if (children[i].classList.contains("froalaeditor-textbox")) {
                    getinputdata = children[i];
                }
                else if (children[i].classList.contains("froalaeditor-textarea")) {
                    getinputdata = children[i];
                }
                else {
                    getinputdata = "";
                }
                if (getinputdata != "") {
                    var inputgroup_gid = getinputdata.id;
                    var inputgroup_name = getinputdata.name;
                    var inputmax_length = (getinputdata.maxLength == -1) ? "" : getinputdata.maxLength
                    var input_placeholder = getinputdata.placeholder;
                    var input_type = getinputdata.type;

                    task.list.push({
                        inputgroup_gid: inputgroup_gid,
                        inputgroup_name: inputgroup_name,
                        inputmax_length: inputmax_length,
                        input_placeholder: input_placeholder,
                        input_type: input_type
                    });
                }
            }
            return task;
        };
        $scope.titleOptions = {

            placeholderText: 'Add a Title',

            charCounterCount: false,

            toolbarInline: true,

            events: {

                'contentChanged': function (e, editor) {

                    console.log('content changed', $scope.titleOptions.froalaEditor.html.get());

                },

                'initialized': function (editor) {

                    console.log('initialized', this);

                }

            }

        };
        new FroalaEditor('div#froala-editor', { 
            heightMin: 900,
            heightMax: 1500, 
            charCounterCount: false,
            quickInsertEnabled: false,
            imageMove: false, 
            imageDefaultWidth: 0, 
            imageDefaultAlign: 'left',
            imageUploadURL: imgurl,
            toolbarButtons: ['Fullscreen', 'bold','italic','underline','strikeThrough',
            'subscript','superscript','fontFamily','fontSize','inlineStyle','paragraphStyle',
            'paragraphFormat','align','formatOL','formatUL','outdent','indent','insertLink','insertImage',
            'insertVideo','insertTable','quote','insertHR','undo', 'redo','clearFormatting',
            'selectAll','html'], 
            key: froalaConfigKey,
            events: {
                initialized: function() {
                  this.edit.off();
                }
              }
        })
        new FroalaEditor('div#froala-editorpreview', { 
            heightMin: 900,
            heightMax: 1500, 
            charCounterCount: false,
            quickInsertEnabled: false,
            imageMove: false, 
            imageDefaultWidth: 0, 
            imageDefaultAlign: 'left',
            imageUploadURL: imgurl,
            toolbarButtons: ['Fullscreen', 'bold','italic','underline','strikeThrough',
            'subscript','superscript','fontFamily','fontSize','inlineStyle','paragraphStyle',
            'paragraphFormat','align','formatOL','formatUL','outdent','indent','insertLink','insertImage',
            'insertVideo','insertTable','quote','insertHR','undo', 'redo','clearFormatting',
            'selectAll','html'], 
            key: froalaConfigKey,
            events: {
                initialized: function() {
                  this.edit.off();
                }
              } 
        })
        $scope.editfielddetail = function (content) {   
            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
            editor1.html.set(content); 
            // var getinputarray =  extractContent(content);  
            // $scope.inputlist = getinputarray.list;
            $scope.showedittemplate = true;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = false;
            $scope.ok = function () {
                $modalInstance.close('closed');
            };  
            lockUI();
            var url = "api/MstTemplate/GetTrnInputList"
            var param = {
                template_gid: $scope.template_gid,
                templatetype_gid: $location.search().application_gid,
            }; 
            SocketService.getparams(url, param).then(function (resp) { 
                if(resp.data.status==true){
                    unlockUI();
                    $scope.inputlist = resp.data.MdlInputDtls;  
                    angular.forEach($scope.inputlist, function (value, key) {
                        value.DBInput = false;
                        if (value.input_type=='checkbox' && value.input_givendata=="true"){
                            value.input_givendata = true;
                        }
                    }); 

                    var url = "api/MstTemplate/GetTrnDBInputList"
                    var param = {
                        template_gid: $scope.template_gid,
                        templatetype_gid: $location.search().application_gid,
                        application_gid: $location.search().application_gid,
                    }; 
                    SocketService.getparams(url, param).then(function (resp) { 
                        if(resp.data.status==true){ 
                            unlockUI();
                            $scope.inputDBlist = resp.data.MdlDBInputDtls;  
                            $scope.DefaultinputDBlist = resp.data.MdlDBInputDtls;
                            angular.forEach($scope.inputDBlist, function (value, key) {
                                value.DBInput = true; 
                            }); 
                            var getDBdropdowndtl = $scope.inputDBlist.filter(function (el) { return el.input_type=="select-one"});
                            $scope.inputlist = $scope.inputlist.concat(getDBdropdowndtl); 
                            
                            var span = document.createElement('span');
                            span.innerHTML = content;   
                            var children = span.querySelectorAll('*'); 
                   for(var i = 0 ; i < children.length ; i++) { 
                       if(children[i].classList.contains("froalaeditor-DBFieldtextbox") )
                       {  
                           var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });   
                           if(getgroupdtl!=null){
                            span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  getgroupdtl[0].input_fieldvalue);
                           } 
                       }  
                   }  
                    var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                    editor1.html.set(span.innerHTML);  
                        } 
                        else
                        unlockUI();
                    });
                }
                else {
                    var url = "api/MstTemplate/GetTrnDBInputList"
                    var param = {
                        template_gid: $scope.template_gid,
                        templatetype_gid: application_gid,
                        application_gid: application_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.inputDBlist = resp.data.MdlDBInputDtls;
                            $scope.DefaultinputDBlist = resp.data.MdlDBInputDtls;
                            angular.forEach($scope.inputDBlist, function (value, key) {
                                value.DBInput = true;
                            });
                            var getDBdropdowndtl = $scope.inputDBlist.filter(function (el) { return el.input_type == "select-one" });
                            $scope.inputlist = getDBdropdowndtl;

                            var span = document.createElement('span');
                            span.innerHTML = content;
                            var children = span.querySelectorAll('*');
                            for (var i = 0; i < children.length; i++) {
                                if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                    var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                    if (getgroupdtl != null) {
                                        span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                    }
                                }
                            }
                            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                            editor1.html.set(span.innerHTML);
                        }
                        else
                            unlockUI();
                    });
                }
                   unlockUI();
            });  

            $scope.updateinputTemplate = function () {
                console.log($scope.inputlist)
                lockUI();
                var url = "api/MstTemplate/PostTrnInputList"
                var param = {
                    template_gid: $scope.template_gid,
                    templatetype_gid: $location.search().application_gid,
                    MdlInputDtls: $scope.inputlist,
                };
                SocketService.post(url, param).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.inputdetails = resp.data.MdlInputDtls;
                        var span = document.createElement('span');
                        span.innerHTML = content;
                        var children = span.querySelectorAll('*');
                        for (var i = 0; i < children.length; i++) {
                            if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null) {
                                    children[i].innerHTML = getgroupdtl[0].input_givendata;
                                    children[i].defaultValue = getgroupdtl[0].input_givendata;
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-dropdown")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null && getgroupdtl.length != 0) {
                                    var options = children[i].options;
                                    for (var j = 0; j < options.length; j++) {
                                        if (options[j].value === getgroupdtl[0].input_givenvalue) {
                                            var replaceValue = " value=\"" + options[j].value + "\" selected=true";
                                            var existingvalue = " value=\"" + options[j].value + "\"";
                                            children[i].innerHTML = children[i].innerHTML.replace(existingvalue, replaceValue);
                                        }
                                    }
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-DBFielddropdown")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid == children[i].id });
                                if (getgroupdtl != null && getgroupdtl[0].input_givenvalue != undefined) {
                                    var options = children[i].options;
                                    var getvalue = "";
                                    for (var j = 0; j < options.length; j++) {
                                        getvalue = options[j].value
                                    }
                                    var option = "";
                                    option += '<option value="">' + getgroupdtl[0].input_givenvalue + "</option>";
                                    var data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="' + children[i].id + '" name="' + children[i].placeholder + '"' +
                                        ' placeholder="' + children[i].placeholder + '" disabled="true" style="font-size:13px;font-family:Calibri;"' +
                                        ' type="dropdown">' + option + "</select>&nbsp;";
                                    children[i].innerHTML = children[i].innerHTML.replace(children[i].innerHTML, data);
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-checkbox")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                var test = children[i];

                                if (getgroupdtl[0].input_givenvalue == "true") {
                                    var replaceValue = " value=\"\" checked=true";
                                    var existingvalue = " value=\"\"";
                                }
                                else {
                                    var replaceValue = " value=\"\"";
                                    var existingvalue = " value=\"\" checked=\"true\"";
                                }
                                children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                            }
                            else if (children[i].classList.contains("froalaeditor-radiobutton")) {
                                var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                                if (getgroupdtl != null && getgroupdtl.length != 0) {
                                    if (children[i].value.trim() === getgroupdtl[0].input_givenvalue.trim()) {
                                        var replaceValue = "value=\"" + getgroupdtl[0].input_givenvalue + "\" checked=\"true\"";
                                        var existingvalue = "value=\"" + getgroupdtl[0].input_givenvalue + "\"";
                                        children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                                    }
                                    else {
                                        var replaceValue = "";
                                        var existingvalue = "checked=\"true\"";
                                        children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                                    }
                                }
                            }
                            else if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                if (getgroupdtl != null) {
                                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                }
                            }
                        }
                        $scope.content = span.innerHTML;
                        var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                        editor1.html.set($scope.content);
                        //var previewtemplate_content = getPreviewcontent($scope.content);
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                });
            }
        }  
        $scope.closetemplate = function(){
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = false; 
            $scope.showdefaulttemplate = true;
            if($scope.DefaultinputDBlist && $scope.DefaultinputDBlist.length!=0){
                var span = document.createElement('span');
                span.innerHTML = content;   
                var children = span.querySelectorAll('*'); 
       for(var i = 0 ; i < children.length ; i++) { 
           if (children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea")) {
               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
               if (getgroupdtl != null) {
                   children[i].innerHTML = getgroupdtl[0].input_givendata;
                   children[i].defaultValue = getgroupdtl[0].input_givendata;
               }
           }
           else if (children[i].classList.contains("froalaeditor-dropdown")) {
               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
               if (getgroupdtl != null && getgroupdtl.length != 0) {
                   var options = children[i].options;
                   for (var j = 0; j < options.length; j++) {
                       if (options[j].value === getgroupdtl[0].input_givenvalue) {
                           var replaceValue = " value=\"" + options[j].value + "\" selected=true";
                           var existingvalue = " value=\"" + options[j].value + "\"";
                           children[i].innerHTML = children[i].innerHTML.replace(existingvalue, replaceValue);
                       }
                   }
               }
           }
           else if (children[i].classList.contains("froalaeditor-DBFielddropdown")) {
               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid == children[i].id });
               if (getgroupdtl != null && getgroupdtl[0].input_givenvalue != undefined) {
                   var options = children[i].options;
                   var getvalue = "";
                   for (var j = 0; j < options.length; j++) {
                       getvalue = options[j].value
                   }
                   var option = "";
                   option += '<option value="">' + getgroupdtl[0].input_givenvalue + "</option>";
                   var data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="' + children[i].id + '" name="' + children[i].placeholder + '"' +
                       ' placeholder="' + children[i].placeholder + '" disabled="true" style="font-size:13px;font-family:Calibri;"' +
                       ' type="dropdown">' + option + "</select>&nbsp;";
                   children[i].innerHTML = children[i].innerHTML.replace(children[i].innerHTML, data);
               }
           }
           else if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
               var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
               if (getgroupdtl != null) {
                   span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
               }
           }
           else if (children[i].classList.contains("froalaeditor-checkbox")) {
               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
               var test = children[i];

               if (getgroupdtl[0].input_givenvalue == "true") {
                   var replaceValue = " value=\"\" checked=true";
                   var existingvalue = " value=\"\"";
               }
               else {
                   var replaceValue = " value=\"\"";
                   var existingvalue = " value=\"\" checked=\"true\"";
               }
               children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
           }
           else if (children[i].classList.contains("froalaeditor-radiobutton")) {
               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
               if (getgroupdtl != null && getgroupdtl.length != 0) {
                   if (children[i].value.trim() === getgroupdtl[0].input_givenvalue.trim()) {
                       var replaceValue = "value=\"" + getgroupdtl[0].input_givenvalue + "\" checked=\"true\"";
                       var existingvalue = "value=\"" + getgroupdtl[0].input_givenvalue + "\"";
                       children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                   }
                   else {
                       var replaceValue = "";
                       var existingvalue = "checked=\"true\"";
                       children[i].outerHTML = children[i].outerHTML.replace(existingvalue, replaceValue);
                   }
               }
           }
       }  
           $scope.content = span.innerHTML;
            }
        }
        $scope.previewcontent = function(templatecontent){
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = true;
            $scope.showdefaulttemplate = false;
            var span = document.createElement('span');
            span.innerHTML = templatecontent;   
            var children = span.querySelectorAll('*'); 
            for(var i = 0 ; i < children.length ; i++) { 
                if(children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea") || children[i].classList.contains("froalaeditor-dropdown"))
                {  
                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  children[i].value); 
               } 
               if(children[i].classList.contains("froalaeditor-DBFieldtextbox") )
                {  
                    var getgroupdtl = $scope.DefaultinputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });   
                    if(getgroupdtl!=null){
                    var data = (getgroupdtl[0].input_givendata==null || getgroupdtl[0].input_givendata=="") ? getgroupdtl[0].input_givendata : getgroupdtl[0].input_fieldvalue;
                     span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  data);
                    } 
                }  
               if($scope.inputlist && $scope.inputlist.length!=0){
                  if(children[i].classList.contains("froalaeditor-DBFielddropdown") ) {  
                             var getgroupdtl = $scope.inputlist.filter(function (el) { return el.input_fieldid == children[i].id });   
                             if(getgroupdtl!=null){
                              span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  getgroupdtl[0].input_givendata);
                             } 
                 }  
               }
            }  
            var editor1 = new FroalaEditor('div#froala-editorpreview', {}, function () { })
            editor1.html.set(span.innerHTML);   
        }
        function getPreviewcontent(templatecontent){
            var span = document.createElement('span');
            span.innerHTML = templatecontent;   
            var children = span.querySelectorAll('*'); 
           for(var i = 0 ; i < children.length ; i++) { 
               if(children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea") || children[i].classList.contains("froalaeditor-dropdown"))
               {  
                   span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  children[i].value); 
               } 
           }
           return span.innerHTML;
        }
        function getPreviewWordcontent(templatecontent){
            var span = document.createElement('span');
            span.innerHTML = templatecontent;   
            var children = span.querySelectorAll('*'); 
           for(var i = 0 ; i < children.length ; i++) { 
               if(children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea") || children[i].classList.contains("froalaeditor-dropdown"))
               {  
                   span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  children[i].value); 
               } 
               if(children[i].classList.contains("froalaeditor-checkbox"))
               {  
                   span.innerHTML = span.innerHTML.replace(children[i].outerHTML,  + "#lsfroalaeditorcheckbox# " + children[i].value); 
               } 
           }
           return span.innerHTML;
        }
        $scope.changeTemplate = function () {
            $scope.content = $scope.cbotemplate.template_content;
        }
        $scope.TemplateConfirm = function(){
            var params = {
                template_gid: $scope.cbotemplate.template_gid,
                templatetype_gid: $location.search().application_gid,
                lstemplatefrom: 'CAM'
            }
            var url = 'api/MstTemplate/PostTrnTemplateConfirm';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                }
            });
        }
    }
})();