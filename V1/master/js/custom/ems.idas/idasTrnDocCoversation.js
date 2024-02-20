(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocCoversation', idasTrnDocCoversation);

    idasTrnDocCoversation.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','cmnfunctionService'];

    function idasTrnDocCoversation($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocCoversation';
        var sanctiondocument_gid;
        activate();

        function activate() {
            $scope.DivFile = false;
            $scope.IsVisible = false;
            $scope.Visible = true;
            sanctiondocument_gid = localStorage.getItem('sanctiondocument_gid');
            $scope.lspage = localStorage.getItem('lspage');

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

            var url = 'api/IdasTrnSanctionDoc/DocConversation';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.docconversationlist = resp.data.MdlDocConversation;
               
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
              
               
            });

            //var params = {
            //    conversationdocument_gid: 'undefine'
            //}
            //var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //});
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
                    var prefix = "http://"
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
            }
        }
        $scope.raiseQuery = function () {
          
                var params = {
                    sanctiondocument_gid: sanctiondocument_gid,
                    sanction_gid: $scope.sanction_gid,
                    document_gid:$scope.document_gid,
                    cad_query: $scope.content
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
                    Notify.alert('Error Occurred!')
                }
                activate();
            });
        }

        $scope.updateFinalRemarks=function()
        {
            var params = {
                sanctiondocument_gid: sanctiondocument_gid,
                scanfinal_remarks: $scope.scanfinal_remarks
            }

            var url = 'api/IdasTrnSanctionDoc/DocScanFinalRemarks';
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
                    Notify.alert('Error Occurred!')
                }
                activate();
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

        $scope.docconback=function()
        {
            if ($scope.lspage=="Maker")
            {
                $state.go('app.idasTrnDocVerifyMkr');
            }
            if ($scope.lspage == "Checker")
            {
                $state.go('app.idasTrnDocVerifyChkr');
            }
            
        }

        $scope.raiseResponse = function (id, count, textArea)
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
    }
})();
