(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnRmResponse', idasTrnRmResponse);

    idasTrnRmResponse.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnRmResponse($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        $scope.title = 'idasTrnRmResponse';
        var sanctiondocument_gid;
        activate();

        function activate() {
            $scope.DivFile = false;
            sanctiondocument_gid = localStorage.getItem('sanctiondocument_gid');
            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.scandocconversation = resp.data.MdlDocConversation;
             
            });

            var url = 'api/IdasTrnPhyDoc/PhyDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.phydocconversation = resp.data.MdlDocConversation;

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
                $scope.scandocument_date = resp.data.scandocument_date;
                $scope.phydocument_date = resp.data.phydocument_date;
                $scope.types_of_copy = resp.data.types_of_copy;
                $scope.phydocument_type = resp.data.phydocument_type;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.phyfinal_remarks = resp.data.phyfinal_remarks;
               
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
        $scope.rmresponseback = function () {

            $state.go('app.idasTrnRmResponseDoc');
        }
        $scope.raiseResponse = function (id, count, textArea) {

           

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

    }
})();
