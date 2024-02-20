(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocCoversationChkr', idasTrnDocCoversationChkr);

    idasTrnDocCoversationChkr.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$anchorScroll','cmnfunctionService'];

    function idasTrnDocCoversationChkr($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService,$anchorScroll,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocCoversationChkr';
        var sanctiondocument_gid;
        activate();

        function activate() {
            $scope.DivFile = false;
            $scope.IsVisible = false;
            $scope.Visible = true;
            $scope.valueExternal = false;
            $scope.valueInternal = false;
            sanctiondocument_gid = localStorage.getItem('sanctiondocument_gid');
            $scope.conversation_count = localStorage.getItem('conversation_count');
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistInternal = resp.data.MdlDocConversation;
                    console.log($scope.docconversationlistInternal);
                    $scope.valueInternal = true;
                } else {
                    $scope.valueInternal = false;

                }


            });

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                    $scope.valueExternal = true;
                } else {
                    $scope.valueExternal = false;

                }


            });
            $scope.typeofcopy = 'Scan Copy';
            var url = 'api/IdasTrnSanctionDoc/GetDocDetailsView';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanction_gid = resp.data.sanction_gid;
                $scope.document_gid = resp.data.document_gid;
                $scope.document_code = resp.data.document_code;
                $scope.document_name = resp.data.document_name;
                $scope.document_date = resp.data.scandocument_date;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.maker_status = resp.data.maker_status;
                $scope.types_of_copy = resp.data.types_of_copy;

                $scope.checker_status = resp.data.checker_status;

                $scope.txtfinalremarks = resp.data.finalremarks;

                if (resp.data.finalremarks == 'Others') {
                    $scope.other_remarks = true;
                } else {
                    $scope.other_remarks = false;
                }
            });

            var url = 'api/IdasTrnSanctionDoc/GetDocComments';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.doc_comments = resp.data.doc_comments;

            });
          
        }

        $scope.onchangeremarks = function (txtfinalremarks) {
            if (txtfinalremarks == 'Others') {
                $scope.other_remarks = true;
            } else {
                $scope.other_remarks = false;
            }
        }

    
        $scope.btncopyinternal=function(index,value)
        {
            $scope.docconversationlistInternal[index].reply=value;
        }
      
        $scope.btnShow = function (id, reply) {
            $scope.IsVisible = true;
            $scope.Visible = false;

        }
        $scope.btnHide = function () {
            $scope.IsVisible = false;
            $scope.Visible = true;
        }
        $scope.PopupDownload = function (docconversation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mailconversation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.downloads = function (val1, val2) {

                    var phyPath = val1;
                    console.log(phyPath);
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = val2.split(".")
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }
              
                var url = "api/IdasTrnDocConversation/GetUploadDoc";
                var params = {
                    docconversation_gid: docconversation_gid
                };
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.viewDocumentList = resp.data.uploaddocument;
                 
                });

                var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                SocketService.get(url).then(function (resp) {

                    $scope.uploaddocument = resp.data.uploaddocument;

                });
            }
        }

       

       
        $scope.raiseQuery = function () {
          
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid:$scope.document_gid,
                    cad_query: $scope.content,
                    document_name: $scope.document_name,
                    document_title: $scope.txtdocument_title,
                    type_of_conversation: 'External'
                }
             
                var url = 'api/IdasTrnSanctionDoc/RaiseConversation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Conversation Added Successfully..!!', 'success')
                        $scope.content = '';
                        activate();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                    activate();
                });

           
        }
        $scope.FileShow = function () {
            if ($scope.DivFile == true) {
                $scope.DivFile = false;
            }
            else {
                $scope.DivFile = true;
            }
        }
        $scope.uploadallocation = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/ConversationDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                   
                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {
                       
                        $scope.uploaddocument = resp.data.uploaddocument;
                      
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.update=function()
        {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                document_date: $scope.document_date
            }
           
            var url = 'api/IdasTrnSanctionDoc/PostScanDocDate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, 'success')
                   
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }
                activate();
            });
        }

        $scope.updateFinalRemarks=function()
        {
            if ($scope.txtfinalremarks == 'Others' && ($scope.scanfinal_remarks == '' || $scope.scanfinal_remarks == undefined || $scope.scanfinal_remarks == null)) {
                Notify.alert('Kindly Enter Remarks', 'warning')
            }
            else {
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    scanfinal_remarks: $scope.scanfinal_remarks,
                    finalremarks: $scope.txtfinalremarks
                }

                var url = 'api/IdasTrnSanctionDoc/DocScanFinalRemarks';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, 'success')
                        activate();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, 'warning'),
                         $scope.txtfinalremarks = '',
                         $scope.scanfinal_remarks = ''
                    }

                });
            }
        }
        $scope.UploadDocCancel = function (conversationdocument_gid) {
            var params = {
                conversationdocument_gid: conversationdocument_gid
            }
            var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully..!!', 'success')
                   
                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.uploaddocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')

                }

            });
        }
        $scope.ChkrVerify = function () {
            var url = 'api/IdasTrnSanctionDoc/DocumentConfirmation';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    localStorage.setItem('conversation_count', '1');
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
              //  activate();
            });
        }
        $scope.docconback=function()
        {
           
                $state.go('app.idasTrnDocVerifyChkr');
          
        }
        $scope.forwardQuery = function (val) {
            var params = {
                docconversation_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to forward this query to RM ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Forward it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/idasTrnMakerCheckerDtls/PostForwardedQuery";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            SweetAlert.swal('Forwarded Successfully!');
                            unlockUI();
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        
        $scope.confirmQuery = function (val,noquery_flag) {
           
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to confirm this query to checker ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Confirmed it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var params = {
                        docconversation_gid: val,
                        rm_response: 'Query Confirmed.'
                    }
                    if (noquery_flag=='Y')
                    {
                        var url = 'api/IdasTrnSanctionDoc/DocNoQueryRmResponse';
                        SocketService.getparams(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                SweetAlert.swal('Document Confirmed Successfully!');
                                unlockUI();

                                activate();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });
                    }
                    else {
                        var url = "api/IdasTrnSanctionDoc/DocRmResponse";
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                SweetAlert.swal('Query Confirmed Successfully!');
                                unlockUI();
                                activate();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });
                    }
                 
                  

                }

            });
        }
        $scope.btncopy = function (data) {

            $scope.content = data;
            $location.hash('down');
            $anchorScroll();
         
        }
       
        $scope.raiseResponse = function (id, textArea)
        {
            var params = {
                docconversation_gid: id,
                rm_response: textArea
            }
            
            var url = 'api/IdasTrnSanctionDoc/DocRmResponse';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Conversation Added Successfully..!!', 'success')
                    $scope.content = " ";
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred!')
                }
                activate();
            });
        }

        $scope.onchangecopy = function (types_of_copy) {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                type_copy: $scope.types_of_copy
            }
            var url = "api/IdasTrnDocConversation/PostTypeOfCopy";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    activate();
                    Notify.alert(resp.data.message)
                }

            });
        }
    }
})();
