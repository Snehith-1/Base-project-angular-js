(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocConversationPhy', idasTrnDocConversationPhy);

    idasTrnDocConversationPhy.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocConversationPhy($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocConversationPhy';
        var sanctiondocument_gid;
        var phydoc_status;
        activate();

        function activate() {
            $scope.DivFile = false;
            $scope.IsVisible = false;
            $scope.Visible = true;
            //$scope.history = false;

            sanctiondocument_gid = localStorage.getItem('sanctiondocument_gid');
            $scope.conversation_count = localStorage.getItem('conversation_count');
            phydoc_status = localStorage.getItem('phydoc_status');
            console.log(phydoc_status);

            if ($scope.conversation_count == '0' && phydoc_status == 'Pending') {
                $scope.showraisequery = false;
                $scope.hideraisequery = false;
            }
            else {
                $scope.showraisequery = true;
                $scope.hideraisequery = true;
            }
          
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1,

            };


            var url = 'api/IdasTrnPhyDoc/PhyDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversation = resp.data.MdlDocConversation;
                 
                } else {
               

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
                $scope.phydocument_date = resp.data.phydocument_date;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.phyfinal_remarks = resp.data.phyfinal_remarks;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.maker_status = resp.data.maker_status;
                $scope.checker_status = resp.data.checker_status;
                $scope.phydoc_status = resp.data.phydoc_status;
                $scope.types_of_copy = resp.data.phydocument_type;
                
            });

        

            var url = 'api/IdasTrnSanctionDoc/GetDocComments';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.doc_comments = resp.data.doc_comments;

            });

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistInternal = resp.data.MdlDocConversation;

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


        }
        $scope.ShowRaiseQuery = function () {
            if ($scope.showraisequery == false) {
                $scope.showraisequery = true;
               }
            else {
                $scope.showraisequery = false;
              }

        }


        $scope.raiseNoQuery = function () {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to Confirm this Document ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Confirm it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/IdasTrnPhyDoc/NoQuery';
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            SweetAlert.swal('Document Confirmed Successfully!');
                            unlockUI();
                            $scope.conversation_count = "1";
                            localStorage.setItem('conversation_count', '1');
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

        //$scope.Conversationhistory=function()
        //{
          
        //    if($scope.history==true)
        //    {
        //        $scope.history = false;
        //    }
        //    else {
        //        $scope.history = true;
        //    }
        //}

        $scope.Verify = function () {
            var url = 'api/IdasTrnPhyDoc/PhyDocVerify';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
        }


        $scope.raiseQueryRM = function () {

            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                sanction_gid: $scope.sanction_gid,
                document_gid: $scope.document_gid,
                cad_query: $scope.content,
                document_name: $scope.document_name,
                document_title: $scope.txtdocument_title,
                type_of_conversation: 'External'
            }

            var url = 'api/IdasTrnPhyDoc/PhyDocCadQuery';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Conversation Added Successfully..!!', 'success')
                    $scope.content = '';
                   
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });


        }

        $scope.raiseResponse = function (id, textArea) {
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

                var url = "api/IdasTrnDocConversation/GetUploadDoc";
                var params = {
                    docconversation_gid: docconversation_gid
                };
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.viewDocumentList = resp.data.uploaddocument;

                });
            }
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

        $scope.updateFinalRemarks = function () {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                phyfinal_remarks: $scope.phyfinal_remarks
            }

            var url = 'api/IdasTrnPhyDoc/DocPhyFinalRemarks';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Final Remarks Updated Successfully..!!', 'success')
                    //$scope.content = " "
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }

        $scope.docconback = function () {

            $state.go('app.idasTrnPhyDocVerification');


        }
        $scope.update = function () {

            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                document_date: $scope.phydocument_date
            }

            var url = 'api/IdasTrnPhyDoc/PostPhyDocDate';
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


        $scope.onchangecopy = function (types_of_copy) {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                type_copy: $scope.types_of_copy
            }
            var url = "api/IdasTrnPhyDoc/PostPhyDocumentType";
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
