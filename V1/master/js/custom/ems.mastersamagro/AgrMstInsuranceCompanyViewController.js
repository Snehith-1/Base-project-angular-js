(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstInsuranceCompanyViewController', AgrMstInsuranceCompanyViewController);

        AgrMstInsuranceCompanyViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstInsuranceCompanyViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstInsuranceCompanyViewController';

        $scope.insurancecompany_gid = $location.search().lsinsurancecompany_gid;
        var insurancecompany_gid = $scope.insurancecompany_gid;

        activate();

        function activate() {

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };

            vm.open2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened2 = true;
            };

            vm.open3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened3 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


            var params = {
                insurancecompany_gid: insurancecompany_gid
            }
            var url = 'api/AgrMstSamAgroMaster/EditInsuranceCompany';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.insurancecompany_gid = resp.data.insurancecompany_gid
                $scope.lblinsurancecompany_name = resp.data.insurancecompany_name;
            });

            var url = 'api/AgrMstSamAgroMaster/PolicyList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.policy_list = resp.data.policy_list;
            });

       
        }

        
        $scope.Back = function ()
        {
            $location.url('app/AgrMstInsuranceCompany');
        }
           

        

       

         $scope.policy_view = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/PolicyView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                $scope.lblpolicy_name = data.policy_name;
                $scope.lblpolicy_number = data.policy_number;
                $scope.lblpolicy_amount = data.policy_amount;
                $scope.lblpolicyperiod_from = data.policyperiod_from;
                $scope.lblpolicyperiod_to = data.policyperiod_to;
                $scope.lblpremium_amount = data.premium_amount;
                $scope.lblpremiumpayment_status = data.premiumpayment_status;
                $scope.lblpaid_date = data.paid_date;
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        
       

       



        $scope.policydoc_view = function (insurancecompany2policy_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/policydocument_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
               policydoclist();

                $scope.policydocumentupload = function (val) {
                    if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                        $("#momdocument").val('');
                        Notify.alert('Kindly Enter the Document Title', 'warning');
                    } 
                    else {
                        var frm = new FormData();
                        for (var i = 0; i < val.length; i++) {
                            var item = {
                                name: val[i].name,
                                file: val[i]
                            };
                            frm.append('fileupload', item.file);
                            frm.append('file_name', item.name);
                            frm.append('document_title', $scope.txtdocument_title);
                            frm.append('insurancecompany2policy_gid', insurancecompany2policy_gid);
                            frm.append('project_flag', "documentformatonly");

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

                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstSamAgroMaster/PolicyDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                               // $scope.policydoc_list = resp.data.policydoc_list;
                                unlockUI();
                                $scope.txtdocument_title = '';
                                $("#policydocupload").val('');
                                $scope.uploadfrm = undefined;

                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });

                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                                policydoclist();
                                unlockUI();
                            });
                        }
                        else {
                            alert('Document is not Available..!');
                            return;
                        }
                    }
                }

                $scope.policydoc_delete = function (insurancecompanypolicy2document_gid) {
                    lockUI();
                    var params = {
                        insurancecompanypolicy2document_gid: insurancecompanypolicy2document_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/PolicyDocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        policydoclist();
                        unlockUI();
                    });
                }

                function policydoclist() {
                    var params = {
                        insurancecompany2policy_gid: insurancecompany2policy_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/PolicyDocumentUploadTmpList';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.policydoc_list = resp.data.policydoc_list;
    
                    });
    
                }

                $scope.download_doc = function (val1, val2) {

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');

                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.policydoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.policydoc_list[i].document_path, $scope.policydoc_list[i].document_name);
                    }
                }
                
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.policydoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.policydoc_list[i].document_path, $scope.policydoc_list[i].document_name);
            }
        }
        
       
    }
})();

