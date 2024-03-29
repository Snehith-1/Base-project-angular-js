(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAddTemplateController', MstAddTemplateController);

    MstAddTemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','$modal'];

    function MstAddTemplateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
    
        $scope.title = 'MstAddTemplateController';
        var vertical_gid = $location.search().lsvertical_gid;
        var vm = this;
        var froalaConfigKey = apiManage.GetCommonData['froalaConfig'].Key;
        activate();

        function activate() {
            $scope.InputgroupList = [];

          var params = {
              vertical_gid: vertical_gid
         }
        var url = 'api/vertical/Getverticalupdate';
        SocketService.getparams(url, params).then(function (resp) { 
            $scope.vertical_codeedit = resp.data.verticalCodeedit;
            $scope.lblvertical_name = resp.data.verticalNameedit; 
        });

            var params = {
                vertical_gid: vertical_gid,
                lstype: '',
                lstypegid: ''
            }
            var url = 'api/SystemMaster/GetVerticalProgramList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.program_list = resp.data.program_list;
                unlockUI();
            });
        }; 

        $scope.submitTemplate = function () {
            lockUI();
            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
            var templatecontent = editor1.html.get();
            if(templatecontent=="")
              {
                Notify.alert("Kindly fill the template content", 'warning');
                unlockUI();
                return false;
              }
            
            var getinputarray =  extractContent(editor1.html.get(),""); 

            var params = {
                template_name: $scope.templatename, 
                vertical_gid: vertical_gid,
                vertical_name: $scope.lblvertical_name,
                template_programlist: $scope.cboprogram,
                template_content: templatecontent,
                templateinputtype_list : getinputarray.list,
                template_type: 'CAM'
            }  
            var url = 'api/MstTemplate/PostTemplateDtl';
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    $location.url('app/MstTemplateSummary?lsvertical_gid='+ vertical_gid);
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }

            });
        }

        function extractContent(s, option) { 
                    var span = document.createElement('span');
                    span.innerHTML = s; 
                    var txtx = span.innerHTML;
                    var children= span.querySelectorAll('*');
                    var task = {
                        list: []
                   } 
                    for(var i = 0 ; i < children.length ; i++) {
                        var inputgroup_name ="",inputgroup_gid="", getinputdata = "",inputlistarray="", input_placeholder=""; 
                        var input_previewtext ="",fieldmapping_gid="";
                      if(children[i].classList.contains("froalaeditor-textbox") && (option=="SmallTextbox" || option=="")){
                        getinputdata = children[i]; 
                      } 
                      else if(children[i].classList.contains("froalaeditor-DBFieldtextbox") && (option=="SmallTextbox" || option=="")){
                        getinputdata = children[i];    
                      }
                      else if(children[i].classList.contains("froalaeditor-textarea") && (option=="LargeTextbox" || option=="")){
                        getinputdata = children[i]; 
                      }  
                      else if(children[i].classList.contains("froalaeditor-dropdown") && (option=="Dropdown" || option=="")){
                        getinputdata = children[i];  
                      }  
                      else if(children[i].classList.contains("froalaeditor-DBFielddropdown") && (option=="Dropdown" || option=="")){
                        getinputdata = children[i];    
                      }
                      else if(children[i].classList.contains("froalaeditor-radiobutton") && (option=="RadioButton" || option=="")){
                        getinputdata = children[i];  
                      }  
                      else if(children[i].classList.contains("froalaeditor-checkbox") && (option=="Checkbox" || option=="")){
                        getinputdata = children[i];  
                      }  
                      else{
                        getinputdata ="";
                      }
                      if(getinputdata!=""){  
                        var input_fieldid = getinputdata.id;
                        var input_fieldname = getinputdata.name; 
                        if($scope.InputgroupList!=null){
                            var getgroupdtl = $scope.InputgroupList.filter(function (el) { return el.input_fieldid == input_fieldid });  
                            if(getgroupdtl!="" && getgroupdtl.length!=0){
                                inputgroup_gid = getgroupdtl[0].inputgroup_gid;
                                inputgroup_name =  getgroupdtl[0].inputgroup_name;
                                inputlistarray = getgroupdtl[0].inputlistarray;
                                input_placeholder = getgroupdtl[0].input_placeholder;
                                input_previewtext = getgroupdtl[0].input_previewtext;
                                fieldmapping_gid = getgroupdtl[0].fieldmapping_gid;
                            } 
                        } 
                        var inputmax_length = (getinputdata.maxLength==-1) ? "" : getinputdata.maxLength
                        //var input_placeholder = getinputdata.placeholder;
                        var input_type =  getinputdata.type;
                        var input_mandatory = getinputdata.required;
                        if(inputmax_length==null) inputmax_length="0";
                        if(task.list.length !=0){
                            var getradioinputdtl = task.list.filter(function (el) { return el.input_fieldid == input_fieldid && input_type=="radio"}); 
                            if(getradioinputdtl.length!=0){ 
                            }
                            else{
                                task.list.push({input_fieldid: input_fieldid, 
                                    input_fieldname: input_fieldname, 
                                    dbfield_type : fieldmapping_gid,
                                    inputgroup_gid: inputgroup_gid, 
                                    inputgroup_name: inputgroup_name,
                                    inputmax_length: inputmax_length,
                                    input_placeholder: input_placeholder,
                                    input_type: input_type,
                                    input_mandatory: input_mandatory,
                                    input_previewtext: input_previewtext,
                                    inputlistarray: inputlistarray});  
                            }
                        }
                        else{
                            task.list.push({input_fieldid: input_fieldid, 
                                input_fieldname: input_fieldname, 
                                dbfield_type : fieldmapping_gid,
                                inputgroup_gid: inputgroup_gid, 
                                inputgroup_name: inputgroup_name,
                                inputmax_length: inputmax_length,
                                input_placeholder: input_placeholder,
                                input_type: input_type,
                                input_mandatory: input_mandatory,
                                input_previewtext: input_previewtext,
                                inputlistarray: inputlistarray});  
                        } 
                      } 
                    }  
                    return task;  
        };

        $scope.back = function () {
            $location.url('app/MstTemplateSummary?lsvertical_gid='+ vertical_gid);  

        }

      
        new FroalaEditor('div#froala-editor', { 
            heightMin: 900,
            heightMax: 1500, 
            charCounterCount: false,
            quickInsertEnabled: false,
            imageMove: false, 
            imageDefaultWidth: 0, 
            imageDefaultAlign: 'left',
            imageUploadURL: imgurl,
            toolbarButtons: ['fullscreen', 'bold','italic','underline','strikeThrough',
            'subscript','superscript','fontFamily','fontSize','inlineStyle','paragraphStyle',
            'paragraphFormat','align','formatOL','formatUL','outdent','indent','insertLink','insertImage',
            'insertVideo','insertTable','quote','insertHR','undo', 'redo','clearFormatting',
            'selectAll','html', 'Textbox'], 
            key: froalaConfigKey,
             
        })
         FroalaEditor.DefineIcon('Textbox', {NAME: 'plus', SVG_KEY: 'add'});
        FroalaEditor.RegisterCommand('Textbox', {
          title: 'Advanced options',
          type: 'dropdown', 
          focus: false,
          undo: false,
          refreshAfterCallback: true, 
          options: {
            'SmallTextbox': 'Small Textbox',
            'LargeTextbox': 'Large Textbox',
            'Dropdown': 'Dropdown',
            'RadioButton': 'RadioButton',
            'Checkbox': 'Checkbox'
          },
          callback: function (cmd, val) {
            var data='';
            var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }) 
            var getinputarray = extractContent(editor1.html.get(),val); 
            editor1.selection.save();
            if(val=='SmallTextbox'){  
                addsmalltextbox(getinputarray);   
            }
            else if(val=='LargeTextbox'){
                addlargetextbox(getinputarray);  
            }
            else if(val=='Dropdown'){
                adddropdownbox(getinputarray); 
            }
            else if(val=='RadioButton'){
                addradiobutton(getinputarray); 
            } 
            else if(val=='Checkbox'){
                addCheckboxbutton(getinputarray); 
            } 
          },
          // Callback on refresh.
          refresh: function ($btn) { 
          },
          // Callback on dropdown show.
          refreshOnShow: function ($btn, $dropdown) {
             
          }
        });

        // FroalaEditor.RegisterCommand('Large Textbox', {
        //     title: 'Small Textbox', 
        //     focus: false,
        //     undo: false,
        //     refreshAfterCallback: false,
        //     callback: function () {
        //       $scope.addsmalltextbox();
        //     }
        //   });

        //   FroalaEditor.RegisterCommand('Radio Button', {
        //     title: 'Small Textbox', 
        //     focus: false,
        //     undo: false,
        //     refreshAfterCallback: false,
        //     callback: function () {
        //       $scope.addsmalltextbox();
        //     }
        //   });
        // FroalaEditor.DefineIcon('smalltextbox', {NAME: 'smalltextbox', SVG_KEY: 'remove'});
        // FroalaEditor.RegisterCommand('smalltextbox', {
        //   title: 'Small Textbox', 
        //   focus: false,
        //   undo: false,
        //   refreshAfterCallback: false,
        //   callback: function () {
        //     $scope.addsmalltextbox();
        //   }
        // });
        
        // FroalaEditor.DefineIcon('largetextbox', {NAME: 'remove', SVG_KEY: 'remove'});
        // FroalaEditor.RegisterCommand('largetextbox', {
        //   title: 'Large Textbox',  
        //   focus: false,
        //   undo: true,
        //   refreshAfterCallback: true,
        //   callback: function () {
        //     this.html.set('');
        //     this.events.focus();
        //   }
        // });
      
        // FroalaEditor.DefineIcon('dropdown', {NAME: 'plus', SVG_KEY: 'add'});
        // FroalaEditor.RegisterCommand('dropdown', {
        //   title: 'Dropdown',
        //   focus: true,
        //   undo: true,
        //   refreshAfterCallback: true,
        //   callback: function () {
        //     this.html.insert('My New HTML');
        //   }
        // });

        // FroalaEditor.DefineIcon('radiobutton', {NAME: 'plus', SVG_KEY: 'add'});
        // FroalaEditor.RegisterCommand('radiobutton', {
        //   title: 'RadioButton',
        //   focus: true,
        //   undo: true,
        //   font_awesome: '<i class="fa fa-[NAME]" aria-hidden="true"></i>,',
        //   refreshAfterCallback: true,
        //   callback: function () {
        //     this.html.insert('My New HTML');
        //   }
        // });
 
 
       function addsmalltextbox (getinputarray) { 
        var modalInstance = $modal.open({
            templateUrl: '/addsmalltextbox.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) { 
            $scope.cbofieldmapping ="";
            $scope.groupnamelist =[];
            var getgroupdata = getinputarray.list;
            if(getgroupdata && getgroupdata.length !=0){
                var getgroupdtl = getgroupdata.filter(function (el) { return el.inputgroup_gid == 0  && (el.dbfield_type == "" || el.dbfield_type == undefined)}); 
                $scope.groupnamelist = getgroupdtl;
            } 
            $scope.groupnamelist.push({ input_fieldid: 0, input_fieldname: 'New Group', inputgroup_gid: 0, inputgroup_name: 'New Group', input_placeholder:'', inputmax_length:'', input_mandatory:''}) 
                      
        $scope.onchangegroup = function (index) { 
            $scope.txtdisabled = false; 
            var getgroupdtl = $scope.groupnamelist.filter(function (el) { return el.input_fieldid === $scope.txtgroup_name.input_fieldid });
            if(getgroupdtl!=null && getgroupdtl[0].input_placeholder!=""){ 
                $scope.txttextbox_name = getgroupdtl[0].input_fieldname;
                $scope.txtplaceholder = getgroupdtl[0].input_placeholder;
                $scope.txtmaxlength  = getgroupdtl[0].inputmax_length;
                $scope.txtdisabled = true;
                $scope.rdbMandatory = (getgroupdtl[0].input_mandatory == "true") ? 'Yes' : 'No' ;
             }
             else{
                $scope.txtplaceholder = '';
                $scope.txtmaxlength  =''; 
                $scope.txttextbox_name ='';
                $scope.rdbMandatory ='';
             }
        }
        $scope.Changedbfield = function(input_type){
            if($scope.rdbInputfield=="ManualCapturingField"){
                $scope.cbofieldmapping ="";
            }
            else{
                lockUI();
                var url = "api/MstTemplate/GetFieldMappingDropdown"
                var param = {
                    input_type: input_type
                }; 
                SocketService.getparams(url, param).then(function (resp) { 
                    if(resp.data.status==true){
                        unlockUI();
                        $scope.fieldmappinglist = resp.data.MdlFieldMappingDropdown;  
                    }
                    else
                       unlockUI();
                }); 
            } 
        }
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            $scope.submit = function () { 
               
                var lsrequired = "", input_fieldid ="",input_fieldname="",inputgroup_gid="",inputgroup_name="";
                var data = '',fieldmapping_gid="", placeholder="";
                if($scope.rdbInputfield=="DBField"){
                     lsrequired = "";
                     input_fieldid= "input" + getFieldId(); 
                     input_fieldname = $scope.cbofieldmapping.fieldmapping_code + "/" +$scope.cbofieldmapping.fieldmapping_name;
                     placeholder = "Choose " + $scope.cbofieldmapping.fieldmapping_name;
                     fieldmapping_gid = $scope.cbofieldmapping.fieldmapping_gid;
                     data = ' &nbsp;<input class="froalaeditor-DBFieldtextbox" id="'+ input_fieldid +'" name="' + input_fieldname +'"' +
                            ' placeholder="'+ input_fieldname +'" disabled="true" style="font-size:13px;font-family:Calibri;" '+lsrequired+'  type="text" maxlength=""/>&nbsp;'; 
                }
                else{
                     lsrequired = ($scope.rdbMandatory == "true" || $scope.rdbMandatory == "Yes") ? 'required' : ''; 
                     input_fieldid= "input" + getFieldId(); 
                     input_fieldname = $scope.txttextbox_name;
                     inputgroup_gid = $scope.txtgroup_name.input_fieldid;
                     inputgroup_name = $scope.txtgroup_name.input_fieldname; 
                     placeholder = $scope.txtplaceholder;
                     data = ' &nbsp;<input class="froalaeditor-textbox" id="'+ input_fieldid +'" name="' + $scope.txttextbox_name +'"' +
                               ' alt="' +  $scope.txtgroup_name.input_fieldid + ",," +$scope.txtgroup_name.input_fieldname +'" '+
                               ' placeholder="'+ $scope.txtplaceholder +'" disabled="true" style="font-size:13px;font-family:Calibri;" '+lsrequired+'  type="text" maxlength="'+ $scope.txtmaxlength +'"/>&nbsp;'; 
                      
                }
                 var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }) 
                 editor1.selection.restore();
                 editor1.html.insert(data, true); 
                $modalInstance.close('closed'); 
                updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,placeholder,"","",fieldmapping_gid);
            } 
           
        }
        }

        function updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,input_placeholder,inputtype,inputlistarray,fieldmapping_gid){
            var input_previewtext = (inputtype =="Checkbox") ? inputlistarray: "";
            inputlistarray = (inputtype =="Checkbox") ? "": inputlistarray;
            $scope.InputgroupList.push({input_fieldid:input_fieldid, 
                 input_fieldname:input_fieldname, 
                 fieldmapping_gid: fieldmapping_gid,
                 inputgroup_gid:inputgroup_gid, 
                 inputgroup_name:inputgroup_name,
                 input_placeholder:input_placeholder, 
                 inputtype:inputtype,
                 input_previewtext: input_previewtext,
                 inputlistarray:inputlistarray}); 
        }
        function getFieldId(){
            var date = new Date();
            var components = [ date.getYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(),  date.getMilliseconds()];
            var fieldid = components.join("");
            return fieldid;
        }

        function addlargetextbox (getinputarray) {
            var modalInstance = $modal.open({
                templateUrl: '/addlargetextbox.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 
            $scope.groupnamelist =[];
            var getgroupdata = getinputarray.list;
            if(getgroupdata && getgroupdata.length !=0){
                var getgroupdtl = getgroupdata.filter(function (el) { return el.inputgroup_gid == 0 }); 
                $scope.groupnamelist = getgroupdtl;
            } 
            $scope.groupnamelist.push({ input_fieldid: 0, input_fieldname: 'New Group', inputgroup_gid: 0, inputgroup_name: 'New Group', input_placeholder:'', inputmax_length:'', input_mandatory:''}) 
           
            $scope.onchangegroup = function (index) { 
                $scope.txtdisabled = false; 
                var getgroupdtl = $scope.groupnamelist.filter(function (el) { return el.input_fieldid === $scope.txtgroup_name.input_fieldid });
                if(getgroupdtl!=null && getgroupdtl[0].input_placeholder!=""){
                    $scope.txttextbox_name = getgroupdtl[0].input_fieldname;
                    $scope.txtplaceholder = getgroupdtl[0].input_placeholder;
                    $scope.txtmaxlength  = getgroupdtl[0].inputmax_length;
                    $scope.txtdisabled = true;
                    $scope.rdbMandatory = (getgroupdtl[0].input_mandatory == "true") ? 'Yes' : 'No' ;
                }
                else {
                   $scope.txtplaceholder = ''; 
                   $scope.txttextbox_name ='';
                   $scope.rdbMandatory ='';
                   $scope.txtmaxlength  =''; 
                }
            }
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () { 
                    var lsrequired = ($scope.rdbMandatory == "true" || $scope.rdbMandatory == "Yes") ? 'required' : '';
                    var input_fieldid= "input" + getFieldId();
                    var input_fieldname = $scope.txttextbox_name;
                    var inputgroup_gid = $scope.txtgroup_name.input_fieldid;
                    var inputgroup_name = $scope.txtgroup_name.input_fieldname; 
                    // this.html.insert(' &nbsp;<textarea class="froalaeditor-textarea" groupselect="LargeText box" id="1674096046492" name="Remarks" placeholder="Enter the remarks"></textarea>');
                    var data = ' &nbsp;<textarea class="froalaeditor-textarea" alt="' +  $scope.txtgroup_name.input_fieldid + ",," +$scope.txtgroup_name.input_fieldname +'" id="'+ input_fieldid +'" name="' + $scope.txttextbox_name +'"' +
                           ' placeholder="'+ $scope.txtplaceholder +'" disabled="true" '+lsrequired+' style="font-size:13px;font-family:Calibri;" type="textarea"></textarea>&nbsp;'; 
                     var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }); 
                     editor1.selection.restore();
                     editor1.html.insert(data, true);  
                    $modalInstance.close('closed');
                    updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,$scope.txtplaceholder,"","","");
                }  
            }
        }
        function adddropdownbox (getinputarray) {
            lockUI();
                var modalInstance = $modal.open({
                    templateUrl: '/adddropdownbox.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {  
                    $scope.groupnamelist =[];
                    $scope.dropdownlistarray = [];
                    var getgroupdata = getinputarray.list;
                    if(getgroupdata && getgroupdata.length !=0){
                        var getgroupdtl = getgroupdata.filter(function (el) { return el.inputgroup_gid == 0 && (el.dbfield_type == "" || el.dbfield_type == undefined)}); 
                        $scope.groupnamelist = getgroupdtl;
                    } 
                    $scope.groupnamelist.push({ input_fieldid: 0, input_fieldname: 'New Group', inputgroup_gid: 0, inputgroup_name: 'New Group', input_placeholder:'', inputmax_length:'', input_mandatory:''}) 
                    unlockUI();  
                $scope.onchangegroup = function (index) { 
                    $scope.txtdisabled = false; 
                    var getgroupdtl = $scope.groupnamelist.filter(function (el) { return el.input_fieldid === $scope.txtgroup_name.input_fieldid });
                    if(getgroupdtl!=null && getgroupdtl[0].input_placeholder!=""){
                        $scope.txttextbox_name = getgroupdtl[0].input_fieldname;
                        $scope.txtplaceholder = getgroupdtl[0].input_placeholder; 
                        $scope.txtmaxlength  = getgroupdtl[0].inputmax_length;
                        $scope.rdbMandatory = (getgroupdtl[0].input_mandatory == "true") ? 'Yes' : 'No' ;
                        $scope.dropdownlistarray =  getgroupdtl[0].inputlistarray;
                        $scope.txtdisabled = true;
                    }
                    else {
                        $scope.txtplaceholder = ''; 
                        $scope.txttextbox_name ='';
                        $scope.rdbMandatory ='';
                        $scope.txtmaxlength  =''; 
                        $scope.dropdownlistarray =[];
                    }
                }
                $scope.Changedbfield = function(input_type){
                    if($scope.rdbInputfield=="ManualCapturingField"){
                        $scope.cbofieldmapping ="";
                    }
                    else{
                        lockUI();
                        var url = "api/MstTemplate/GetFieldMappingDropdown"
                        var param = {
                            input_type: input_type
                        }; 
                        SocketService.getparams(url, param).then(function (resp) { 
                            if(resp.data.status==true){
                                unlockUI();
                                $scope.fieldmappinglist = resp.data.MdlFieldMappingDropdown;  
                            }
                            else
                               unlockUI();
                        }); 
                    } 
                }
                $scope.addlist = function () { 
                    $scope.dropdownlistarray.push({
                        preview_text: $scope.txtPreviewText,
                        Value: $scope.txtValue
                    });
                    $scope.txtPreviewText = "";
                    $scope.txtValue = "";
                }
                $scope.deletelist = function (index) {
                    $scope.dropdownlistarray.splice(index, 1);
                }
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.submit = function () { 
                        var lsrequired = "", input_fieldid ="",input_fieldname="",inputgroup_gid="",inputgroup_name="";
                        var data = '',fieldmapping_gid="",placeholder="";
                   if($scope.rdbInputfield=="DBField"){
                       lsrequired = "";
                       input_fieldid= "input" + getFieldId(); 
                       input_fieldname = $scope.cbofieldmapping.fieldmapping_code + "/" +$scope.cbofieldmapping.fieldmapping_name;
                       placeholder = "Choose " + $scope.cbofieldmapping.fieldmapping_name;
                       fieldmapping_gid = $scope.cbofieldmapping.fieldmapping_gid;
                       var option ="";
                       option += '<option value="">' + placeholder + "</option>";

                        data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="'+ input_fieldid +'" name="' + input_fieldname +'"' +
                        ' placeholder="'+ placeholder +'" disabled="true" '+lsrequired+' style="font-size:13px;font-family:Calibri;"' +
                        ' type="dropdown">"' + option + "'</select>&nbsp;";
                   }
                   else{
                        lsrequired = ($scope.rdbMandatory == "true" || $scope.rdbMandatory == "Yes") ? 'required' : '';
                        input_fieldid= "input" + getFieldId();
                        input_fieldname = $scope.txttextbox_name;
                        inputgroup_gid = $scope.txtgroup_name.input_fieldid;
                        inputgroup_name = $scope.txtgroup_name.input_fieldname; 
                        placeholder = $scope.txtplaceholder;
                        var option ="";
                        option += '<option value="">' + $scope.txtplaceholder + "</option>";
                        angular.forEach($scope.dropdownlistarray, function (value, key) {
                            if (value.preview_text){
                                option += '<option value="'+ value.Value +'">' +value.preview_text + "</option>'";
                            }
                        }); 
                         data = ' &nbsp;<select class="froalaeditor-dropdown" alt="' +  $scope.txtgroup_name.input_fieldid + ",," +$scope.txtgroup_name.input_fieldname +'" id="'+ input_fieldid +'" name="' + $scope.txttextbox_name +'"' +
                        ' placeholder="'+ $scope.txtplaceholder +'" disabled="true" '+lsrequired+' style="font-size:13px;font-family:Calibri;"' +
                        ' type="dropdown">"' + option + "'</select>&nbsp;";
                    }
                        
                         var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }); 
                         editor1.selection.restore();
                         editor1.html.insert(data, true); 
                        $modalInstance.close('closed'); 
                        updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,placeholder,'Dropdown',$scope.dropdownlistarray,fieldmapping_gid);
                    }  
                }
      }

      function addradiobutton (getinputarray) {
        lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/addradiobutton.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {  
                $scope.groupnamelist =[];
                $scope.radiobuttonlistarray = [];
                var getgroupdata = getinputarray.list;
                if(getgroupdata && getgroupdata.length !=0){
                    var getgroupdtl = getgroupdata.filter(function (el) { return el.inputgroup_gid == 0 }); 
                    $scope.groupnamelist = getgroupdtl;
                } 
                $scope.groupnamelist.push({ input_fieldid: 0, input_fieldname: 'New Group', inputgroup_gid: 0, inputgroup_name: 'New Group', input_placeholder:'', inputmax_length:'', input_mandatory:''}) 
                unlockUI();  
            $scope.onchangegroup = function (index) { 
                $scope.txtdisabled = false; 
                var getgroupdtl = $scope.groupnamelist.filter(function (el) { return el.input_fieldid === $scope.txtgroup_name.input_fieldid });
                if(getgroupdtl!=null && getgroupdtl[0].input_fieldid !=""){
                    $scope.txttextbox_name = getgroupdtl[0].input_fieldname;
                    //$scope.txtplaceholder = getgroupdtl[0].input_placeholder; 
                    $scope.txtmaxlength  = getgroupdtl[0].inputmax_length;
                    $scope.rdbMandatory = (getgroupdtl[0].input_mandatory == "true") ? 'Yes' : 'No' ;
                    $scope.radiobuttonlistarray =  getgroupdtl[0].inputlistarray;
                    $scope.txtdisabled = true;
                }
                else {
                    $scope.txtplaceholder = ''; 
                    $scope.txttextbox_name ='';
                    $scope.rdbMandatory ='';
                    $scope.txtmaxlength  =''; 
                    $scope.radiobuttonlistarray = [];
                }
            }
            $scope.addlist = function () { 
                $scope.radiobuttonlistarray.push({
                    preview_text: $scope.txtPreviewText,
                    Value: $scope.txtValue
                });
                $scope.txtPreviewText = "";
                $scope.txtValue = "";
            }
            $scope.deletelist = function (index) {
                $scope.radiobuttonlistarray.splice(index, 1);
            }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () { 
                    var lsrequired = ($scope.rdbMandatory == "true" || $scope.rdbMandatory == "Yes") ? 'required' : '';
                    var input_fieldid= "input" + getFieldId();
                    var input_fieldname = $scope.txttextbox_name;
                    var inputgroup_gid = $scope.txtgroup_name.input_fieldid;
                    var inputgroup_name = $scope.txtgroup_name.input_fieldname; 

                    var option =""; 
                    angular.forEach($scope.radiobuttonlistarray, function (value, key) {
                        if (value.preview_text){
                            option += ' &nbsp;<span><input class="froalaeditor-radiobutton" alt="' +  $scope.txtgroup_name.input_fieldid + ",," +$scope.txtgroup_name.input_fieldname +'" id="'+ input_fieldid +'" name="' + $scope.txttextbox_name +'"' +
                            ' placeholder="'+ $scope.txtplaceholder +'" value="'+ value.Value +'" disabled="true" '+lsrequired+' style="font-size:13px;font-family:Calibri;"' +
                            ' type="radio"/>' + value.preview_text + "&nbsp;</span>";
                        }
                    }); 
                    var data = option;
                     var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }); 
                     editor1.selection.restore();
                     editor1.html.insert(data, true); 
                    $modalInstance.close('closed'); 
                    updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,$scope.txtplaceholder,"Radiobutton",$scope.radiobuttonlistarray,"");
                }  
            }
  }

  
  function addCheckboxbutton (getinputarray) { 
    lockUI();
    var modalInstance = $modal.open({
        templateUrl: '/addCheckboxbutton.html',
        controller: ModalInstanceCtrl,
        backdrop: 'static',
        keyboard: false,
        size: 'md'
    });
    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
    function ModalInstanceCtrl($scope, $modalInstance) {  
        $scope.groupnamelist =[];
        $scope.radiobuttonlistarray = [];
        var getgroupdata = getinputarray.list;
        if(getgroupdata && getgroupdata.length !=0){
            var getgroupdtl = getgroupdata.filter(function (el) { return el.inputgroup_gid == 0 }); 
            $scope.groupnamelist = getgroupdtl;
        } 
        $scope.groupnamelist.push({ input_fieldid: 0, input_fieldname: 'New Group', inputgroup_gid: 0, inputgroup_name: 'New Group', input_placeholder:'', inputmax_length:'', input_mandatory:''}) 
        unlockUI();  
    $scope.onchangegroup = function (index) { 
        $scope.txtdisabled = false; 
        var getgroupdtl = $scope.groupnamelist.filter(function (el) { return el.input_fieldid === $scope.txtgroup_name.input_fieldid });
        if(getgroupdtl!=null && getgroupdtl[0].input_fieldid !=""){
            $scope.txttextbox_name = getgroupdtl[0].input_fieldname; 
            $scope.rdbMandatory = (getgroupdtl[0].input_mandatory == "true" || getgroupdtl[0].input_mandatory == true) ? 'Yes' : 'No' ;
            $scope.txtPreviewText =  getgroupdtl[0].input_previewtext;
            $scope.txtdisabled = true;
        }
        else {
            $scope.txtPreviewText = ''; 
            $scope.txttextbox_name ='';
            $scope.rdbMandatory ='';
            $scope.txtmaxlength  ='';  
        }
    }
    
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
        $scope.submit = function () { 
            var lsrequired = ($scope.rdbMandatory == "true" || $scope.rdbMandatory == "Yes") ? 'required' : '';
            var input_fieldid= "input" + getFieldId();
            var input_fieldname = $scope.txttextbox_name;
            var inputgroup_gid = $scope.txtgroup_name.input_fieldid;
            var inputgroup_name = $scope.txtgroup_name.input_fieldname; 

            var data = ' &nbsp;<span><input class="froalaeditor-checkbox" alt="' +  $scope.txtgroup_name.input_fieldid + ",," +$scope.txtgroup_name.input_fieldname +'" id="'+ input_fieldid +'" name="' + $scope.txttextbox_name +'"' +
            ' placeholder="'+ $scope.txtplaceholder +'" value="" disabled="true" '+lsrequired+' style="font-size:13px;font-family:Calibri;"' +
            ' type="checkbox"/><span for="' + $scope.txttextbox_name +'">&nbsp;' + $scope.txtPreviewText +"</span> &nbsp;</span>";
             var editor1 = new FroalaEditor('div#froala-editor', {}, function () { }); 
             editor1.selection.restore();
             editor1.html.insert(data, true);
            $modalInstance.close('closed'); 
            updateInputGroupArray(input_fieldid,input_fieldname, inputgroup_gid, inputgroup_name,$scope.txtplaceholder,"Checkbox",$scope.txtPreviewText,"");
        }  
    }
   } 
    }
})();