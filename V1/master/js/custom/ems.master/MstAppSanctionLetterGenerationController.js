(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAppSanctionLetterGenerationController', MstAppSanctionLetterGenerationController);

    MstAppSanctionLetterGenerationController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function MstAppSanctionLetterGenerationController($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAppSanctionLetterGenerationController';

        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.sanction_gid = $location.search().sanction_gid;
        var sanction_gid = $location.search().sanction_gid;
        $scope.lsresubmit = $location.search().lsresubmit;
        var froalaConfigKey = apiManage.GetCommonData['froalaConfig'].Key;
        var lsresubmit = $scope.lsresubmit;
        if (localStorage.getItem('RefreshTemplate') && localStorage.getItem('RefreshTemplate') != 'Y') {
            location.reload();
            localStorage.setItem('RefreshTemplate', 'Y');
            return false;
        }
        activate();
        function activate() {
            $scope.showedittemplate = false;
            $scope.showpreviewtemplate = false;
            $scope.showdefaulttemplate = true;
            lockUI();
            var url = 'api/MstCadFlow/CADSanctionDtls';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.customerNameEdit = resp.data.sanctionto_name;
            });

            //var url = 'api/MstCAD/GetCADLoanFacilityTemplateList';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.template_list = resp.data.cadtemplate_list;
            //});  

            var url = 'api/MstCAD/GetCADTemplateDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionletter_status = resp.data.sanctionletter_status;
                $scope.template_name = resp.data.template_name;
                $scope.template_gid = resp.data.template_gid;  
                $scope.lspath = resp.data.makerfile_path;
                $scope.lsname = resp.data.makerfile_name;
                $scope.showmstcontent = resp.data.mstcontent_flag;
                if(resp.data.mstcontent_flag=="Y"){ 
                    $scope.MdlTemplatelist = resp.data.MdlTemplatedtl;   
                } 
                $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                    if($scope.sanctionletter_flag == 'Y')
                      $scope.content = resp.data.template_content; 
                    else
                      $scope.content = resp.data.defaulttemplate_content; 
                document.getElementById('sanctiontemplate').innerHTML += $scope.content;
                $scope.checkerpushback_remarks = resp.data.checkerpushback_remarks;
                $scope.sanction_template = true;
                $scope.sanction_template_bind = true;
                // if (resp.data.sanctionletter_status == 'Generated' || resp.data.sanctionletter_status == 'Saved') {
                //     $scope.sanction_template = true;
                //     $scope.sanction_template_bind = true; 
                // }
                // else{
                //     $scope.sanction_template = false;
                //     $scope.sanction_template_bind = false;
                // }
                // else {
                //     var param = {
                //         sanction_gid: sanction_gid,
                //         template_gid: $scope.template_gid,
                //         template_name: $scope.template_name,
                //     };
                //     var url = 'api/MstCadFlow/SanctionCommonTemplate';
                //     SocketService.post(url, param).then(function (resp) {
                //         $scope.content = resp.data.template_content;
                //         unlockUI();
                //         $scope.sanction_template = true;
                //         $scope.sanction_template_bind = true;
                //         $scope.template_name = resp.data.template_name;
                //     });
                //     $scope.sanction_template = false;
                //     $scope.sanction_template_bind = false;
                // }
                unlockUI();
            });

            var url = 'api/MstCAD/CADSanctionLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var params = {
                application_gid: application_gid
            };

            var url = 'api/MstCAD/GetSanction';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionuploaddocument_list = resp.data.sanctiondocument_list;
            });
         
        } 

     
      

        $scope.sanctionletterback = function () {
            if (lspage == 'SanctionApprovalCompleted') {
                $location.url('app/MstSanctionDtlViewSummary?sanction_gid=' + sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=rewubmit_flag');
            }
            else {
                $location.url('app/MstSanctionDtlSummary?application_gid=' + application_gid + '&lsemployee_gid=' + employee_gid + '&lspage=SanctionMaker');
            }
           
        }

        // Template Updation
        $scope.sanctiontemplatesubmit = function () {
            lockUI();

            if ($scope.cbotemplate.template_name == 'Sanction - Simplified Norms Single Facility') {
                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                };
                var url = 'api/MstCadFlow/SanctionContent';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

                //else if ($scope.cbotemplate.template_name == 'Sanction - Simplified Norms 2 Facility') {

                //    var param = {
                //        sanction_gid: sanction_gid,
                //        template_gid: $scope.cbotemplate.template_gid,
                //        template_name: $scope.cbotemplate.template_name,
                //    };
                //    var url = 'api/MstCAD/Sanction2Facility';
                //    SocketService.post(url, param).then(function (resp) {
                //        $scope.content = resp.data.template_content;
                //        unlockUI();
                //        $scope.sanction_template = true;
                //        $scope.sanction_template_bind = true;
                //        $scope.template_name = resp.data.template_name
                //    });

                //}

            else if ($scope.cbotemplate.template_name == 'Sanction - Multiple Facility') {

                var param = {
                    sanction_gid: sanction_gid,
                    template_gid: $scope.cbotemplate.template_gid,
                    template_name: $scope.cbotemplate.template_name,
                };
                var url = 'api/MstCadFlow/SanctionMultipleFacility';
                SocketService.post(url, param).then(function (resp) {
                    $scope.content = resp.data.template_content;
                    unlockUI();
                    $scope.sanction_template = true;
                    $scope.sanction_template_bind = true;
                    $scope.template_name = resp.data.template_name
                });

            }

                //else if ($scope.cbotemplate.template_name == 'Sanction - Interchangability') {

                //    var param = {
                //        sanction_gid: sanction_gid,
                //        template_gid: $scope.cbotemplate.template_gid,
                //        template_name: $scope.cbotemplate.template_name,
                //    };
                //    var url = 'api/MstCAD/SanctionContent';
                //    SocketService.post(url, param).then(function (resp) {
                //        $scope.content = resp.data.template_content;
                //        unlockUI();
                //        $scope.sanction_template = true;
                //        $scope.sanction_template_bind = true;
                //        $scope.template_name = resp.data.template_name
                //    });

                //}

                //else if ($scope.cbotemplate.template_name == 'Sanction - DBS Colending') {

                //    var param = {
                //        sanction_gid: sanction_gid,
                //        template_gid: $scope.cbotemplate.template_gid,
                //        template_name: $scope.cbotemplate.template_name,
                //    };
                //    var url = 'api/MstCAD/DBSColending';
                //    SocketService.post(url, param).then(function (resp) {
                //        $scope.content = resp.data.template_content;
                //        unlockUI();
                //        $scope.sanction_template = true;
                //        $scope.sanction_template_bind = true;
                //        $scope.template_name = resp.data.template_name
                //    });

                //}

                //else if ($scope.cbotemplate.template_name == 'Sanction - Stand by line of credit') {

                //    var param = {
                //        sanction_gid: sanction_gid,
                //        template_gid: $scope.cbotemplate.template_gid,
                //        template_name: $scope.cbotemplate.template_name,
                //    };
                //    var url = 'api/MstCAD/SanctionContent';
                //    SocketService.post(url, param).then(function (resp) {
                //        $scope.content = resp.data.template_content;
                //        unlockUI();
                //        $scope.sanction_template = true;
                //        $scope.sanction_template_bind = true;
                //        $scope.template_name = resp.data.template_name
                //    });

                //}
            else {
                Notify.alert("Error occurred", 'warning');
            }
            $scope.cancel = function () {
                $scope.sanction_template = false;
                $scope.sanction_template_bind = false;
            }
        }

        $scope.download_signature = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }
        // Sanction Letter Save Event
        $scope.sanctionletterSave = function () {
            lockUI();
            var previewtemplate_content = getPreviewcontent($scope.content);

            var param = {
                sanction_gid: sanction_gid,
                template_content: previewtemplate_content,
                defaulttemplate_content: $scope.content
            };
            var url = 'api/MstCAD/CADSanctionLetterSave';
            SocketService.post(url, param).then(function (resp) {
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
                    unlockUI();
                }
            });
        }

        function extractContent(s) {
            var span = document.createElement('span');
            span.innerHTML = s; 
            var txtx = span.innerHTML;
            var children= span.querySelectorAll('*');
            var task = {
                list: []
           } 
            for(var i = 0 ; i < children.length ; i++) {
                var getinputdata = "";
              if(children[i].classList.contains("froalaeditor-textbox")){
                getinputdata = children[i]; 
              } 
              else if(children[i].classList.contains("froalaeditor-textarea")){
                getinputdata = children[i]; 
              }  
              else{
                getinputdata ="";
              }
              if(getinputdata!=""){
                var inputgroup_gid = getinputdata.id;
                var inputgroup_name = getinputdata.name; 
                var inputmax_length = (getinputdata.maxLength==-1) ? "" : getinputdata.maxLength
                var input_placeholder = getinputdata.placeholder;
                var input_type =  getinputdata.type;
              
                task.list.push({inputgroup_gid:inputgroup_gid, 
                    inputgroup_name: inputgroup_name, 
                    inputmax_length: inputmax_length,
                    input_placeholder:input_placeholder,
                    input_type: input_type});  
              } 
            }  
            return task;
};


        // Sanction Letter Submit Event
        $scope.sanctionletterSubmit = function () {
            lockUI();
            var previewtemplate_content = getPreviewcontent($scope.content);
            var param = {
                sanction_gid: sanction_gid,
                template_content: previewtemplate_content,
                defaulttemplate_content: $scope.content
            };
            var url = 'api/MstCAD/CADSanctionLetterSubmit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $location.hash('sanctionlettertopview');
                    $anchorScroll();
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

        // Sanction Letter View
        $scope.sanctionletterview = function () {
            $location.url('app/MstAppSanctionLetterWordView?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=' + lspage );
        }

        // Sanction Letter Moved to Checker
        $scope.proceedtochecker = function () {
            lockUI();
            var previewtemplate_content = getPreviewcontent($scope.content);
            var param = {
                sanction_gid: sanction_gid,
                template_content: previewtemplate_content,
                application_gid: application_gid
            };
            var url = 'api/MstCAD/PostProceedToChecker';
            SocketService.post(url, param).then(function (resp) {
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
                    unlockUI();
                }
            });
        }

        // Sanction Letter View
        $scope.sanctiontocheckerview = function (sanctionapprovallog_gid, application2sanction_gid) {
            localStorage.setItem('RefreshTemplate', 'N');
            $location.url('app/MstAppSanctionLetterWordView?sanctionapprovallog_gid=' + sanctionapprovallog_gid + '&sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        }
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
        $scope.initialize = function (initControls) {
            $scope.initControls = initControls;
            $scope.deleteAll = function () {
                initControls.getEditor().html.set('34434');
            };
        };
        $scope.imgModel = { src: 'image.jpg' };
        $scope.buttonModel = { innerHTML: 'Click Me' };
        $scope.inputModel = { placeholder: 'I am an input!' };
        $scope.inputOptions = {
            angularIgnoreAttrs: ['class', 'ng-model', 'id', 'froala']
        }
        $scope.initializeLink = function (linkInitControls) {
            $scope.linkInitControls = linkInitControls;
        };
        $scope.linkModel = { href: 'https://www.froala.com/wysiwyg-editor' }
        //Sanction Document Upload
        $scope.sanctiondocumentUpload = function (val) {
            if (($scope.txtsanctiondocument_title == null) || ($scope.txtsanctiondocument_title == '') || ($scope.txtsanctiondocument_title == undefined)) {
                $("#momdocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }

                }
                frm.append('document_title', $scope.txtsanctiondocument_title);
                frm.append('application_gid', $scope.application_gid);
                frm.append('application2sanction_gid', $scope.sanction_gid);
                frm.append('project_flag', "Default");

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCAD/SanctionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.sanctionuploaddocument_list = resp.data.sanctiondocument_list;
                        unlockUI();

                        $("#sanctiondocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.$parent.txtsanctiondocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deletesanction = function (val) {
            var params = {
                application2sanctiondoc_gid: val,
                application_gid: application_gid
            };

            var url = 'api/MstCAD/SanctionDocDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.sanctionuploaddocument_list = resp.data.sanctiondocument_list;

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.sanctionuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.sanctionuploaddocument_list[i].document_path, $scope.sanctionuploaddocument_list[i].document_name);
            }
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
                templatetype_gid: sanction_gid,
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
                        templatetype_gid: sanction_gid,
                        application_gid: application_gid
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
                        templatetype_gid: sanction_gid,
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

            $scope.updateinputTemplate =function(){ 
               // console.log($scope.inputlist)
                lockUI(); 
                 var url = "api/MstTemplate/PostTrnInputList"
                 var param = {
                    template_gid: $scope.template_gid,
                    templatetype_gid: sanction_gid,
                    MdlInputDtls: $scope.inputlist, 
                 };  
                 SocketService.post(url, param).then(function (resp) { 
                     if(resp.data.status==true){
                        
                       
                        $scope.inputdetails= resp.data.MdlInputDtls;
                        var span = document.createElement('span');
                        span.innerHTML = content;   
                        var children = span.querySelectorAll('*'); 
                        for(var i = 0 ; i < children.length ; i++) { 
                            if(children[i].classList.contains("froalaeditor-textbox") || children[i].classList.contains("froalaeditor-textarea"))
                            { 
                            var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                            if(getgroupdtl!=null){
                                children[i].innerHTML = getgroupdtl[0].input_givendata;  
                                children[i].defaultValue = getgroupdtl[0].input_givendata;  
                            }  
                           }
                           else if(children[i].classList.contains("froalaeditor-dropdown")){ 
                            var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                            if(getgroupdtl!=null && getgroupdtl.length !=0){ 
                                var options = children[i].options; 
                            for (var j = 0 ; j < options.length; j++) {
                              if (options[j].value === getgroupdtl[0].input_givenvalue) {
                                var replaceValue = " value=\""+ options[j].value + "\" selected=true";
                                var existingvalue = " value=\""+ options[j].value + "\"";
                                children[i].innerHTML = children[i].innerHTML.replace(existingvalue,  replaceValue); 
                              } 
                            }
                              }   
                           }
                           else if(children[i].classList.contains("froalaeditor-DBFielddropdown") )
                           {  
                               var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid == children[i].id });   
                               if(getgroupdtl!=null && getgroupdtl[0].input_givenvalue!=undefined){
                                var options = children[i].options; 
                                var getvalue ="";
                                for (var j = 0 ; j < options.length; j++) {
                                    getvalue = options[j].value  
                                }
                                var option ="";
                                option += '<option value="">' + getgroupdtl[0].input_givenvalue + "</option>"; 
                                var data = ' &nbsp;<select class="froalaeditor-DBFielddropdown"   id="'+ children[i].id +'" name="' + children[i].placeholder +'"' +
                                 ' placeholder="'+ children[i].placeholder +'" disabled="true" style="font-size:13px;font-family:Calibri;"' +
                                 ' type="dropdown">' + option + "</select>&nbsp;";  
                                 children[i].innerHTML = children[i].innerHTML.replace(children[i].innerHTML,  data);
                               } 
                            }  
                            else if (children[i].classList.contains("froalaeditor-DBFieldtextbox")) {
                                var getgroupdtl = $scope.inputDBlist.filter(function (el) { return el.input_fieldid == children[i].id });
                                if (getgroupdtl != null) {
                                    span.innerHTML = span.innerHTML.replace(children[i].outerHTML, getgroupdtl[0].input_fieldvalue);
                                }
                            } 
                           else if (children[i].classList.contains("froalaeditor-checkbox")){
                            var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                            var test = children[i];

                            if(getgroupdtl[0].input_givenvalue=="true"){
                                var replaceValue = " value=\"\" checked=true";
                                var existingvalue = " value=\"\"";
                            }  
                            else{
                                var replaceValue = " value=\"\"";
                                var existingvalue = " value=\"\" checked=\"true\"";
                            }
                            children[i].outerHTML = children[i].outerHTML.replace(existingvalue,  replaceValue); 
                           }
                           else if(children[i].classList.contains("froalaeditor-radiobutton")){
                            var getgroupdtl = $scope.inputdetails.filter(function (el) { return el.input_fieldid === children[i].id });
                            if(getgroupdtl!=null && getgroupdtl.length !=0){
                                if(children[i].value.trim() ===getgroupdtl[0].input_givenvalue.trim()){  
                                    var replaceValue = "value=\""+ getgroupdtl[0].input_givenvalue + "\" checked=\"true\"";
                                    var existingvalue = "value=\""+ getgroupdtl[0].input_givenvalue + "\""; 
                                    children[i].outerHTML = children[i].outerHTML.replace(existingvalue,  replaceValue);  
                                }
                                else{
                                    var replaceValue = "";
                                    var existingvalue = "checked=\"true\""; 
                                    children[i].outerHTML = children[i].outerHTML.replace(existingvalue,  replaceValue);
                                } 
                            }   
                          }
                        }    
                        $scope.content = span.innerHTML;
                        var editor1 = new FroalaEditor('div#froala-editor', {}, function () { })
                        editor1.html.set($scope.content); 
                        var previewtemplate_content = getPreviewcontent($scope.content);
                         Notify.alert(resp.data.message, {
                             status: 'success',
                             pos: 'top-center',
                             timeout: 3000
                         });
                        var param = {
                            sanction_gid: sanction_gid,
                            template_content: previewtemplate_content,
                            defaulttemplate_content: $scope.content
                         };
                        
                        var url = 'api/MstCAD/CADSanctionLetterSave';
                        SocketService.post(url, param).then(function (resp) { 
                             unlockUI();
                        });
                        // console.log($scope.content);
                        // console.log('innerHTML',span.innerHTML);
                        // $('div#froala-editor').FroalaEditor('html.set', span.innerHTML);
                        //$scope.refreshfielddetail();
                        
                     }
                     else{
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
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.changeTemplate =function(){
            $scope.content = $scope.cbotemplate.template_content;   
        }
        $scope.TemplateConfirm = function(){
            var params = {
                template_gid: $scope.cbotemplate.template_gid,
                templatetype_gid: sanction_gid,
                lstemplatefrom: 'Sanction'
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
