(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadUrnAcceptedCustomersController', MstCadUrnAcceptedCustomersController);

    MstCadUrnAcceptedCustomersController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstCadUrnAcceptedCustomersController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadUrnAcceptedCustomersController';

        activate();
        
        function activate() {
            var url = 'api/MstCAD/URNTagCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.taggednpa_count = resp.data.taggednpa_count;
                $scope.taggedlegal_count = resp.data.taggedlegal_count;
            });
            var url = 'api/MstCAD/GetCADUrnGroupingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;
                unlockUI();
            }
            else if (resp.data.status == false)
            unlockUI();
            });
            var url = 'api/MstCAD/CADApplicationCount';
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.urngrouping_count = resp.data.urngrouping_count;
            });
        }

        $scope.npareport = function () {
            lockUI();
            var url = 'api/MstCAD/ExportNpaReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

        $scope.legalreport = function () {
            lockUI();
            var url = 'api/MstCAD/ExportLegalReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.view_dtls = function (val, customer_urn) {
            $location.url('app/MstCadUrnAcceptedCustomerDtls?application_gid=' + val + '&customer_urn=' + customer_urn + '&lspage=CADUrnAcceptanceCustomer');
        }

        $scope.edit = function (val) {
            $location.url('app/MstCADApplicationEdit?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.pendincad_review = function () {
            $location.url('app/MstPendingCADReview');
        }

        $scope.urn_grouping = function () {
            $location.url('app/MstCadUrnAcceptedCustomers');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/MstCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/MstSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/MstSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/MstCCRejectedApplications');
        }

        $scope.assignment_view = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assignmentdtl_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application_gid: application_gid
                  }
                var url = 'api/MstCAD/GetAssignmentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.assignment_list = resp.data.assignment_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }


            }

        }
        $scope.urn_tag = function (customer_name,customer_urn,customer2tag_gid,legal_flag,npa_flag) {
            var modalInstance = $modal.open({
                templateUrl: '/application_tag.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    customer2tag_gid : customer2tag_gid
                }
                var url = 'api/MstCAD/URNNPAtagHistory';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    unlockUI();
                    $scope.npatag_list = resp.data.customerurntag_list;
                });
                var url = 'api/MstCAD/URNLegaltagHistory';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    unlockUI();
                    $scope.legaltag_list = resp.data.customerurntag_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.description= function (description){
                    var modalInstance = $modal.open({
                        templateUrl: '/description.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.description=description;
                        $scope.back = function () {
                            $modalInstance.close('closed');
                        }; 
                    }
                }
                $scope.urn_tag_submit = function () {
                    if($scope.cbocustomer_tag == 'NPA' && npa_flag == 'Y'){
                        Notify.alert('NPA already tagged', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else if($scope.cbocustomer_tag == 'Legal' && legal_flag == 'Y'){
                        Notify.alert('Legal already tagged', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else{
                    var params =
                    {
                        tag_type : $scope.cbocustomer_tag,
                        currentcustomer_urn: customer_urn,
                        customer_name : customer_name,
                        tag_remarks : $scope.txttaggedremarks,
                        customer2tag_gid : customer2tag_gid
                    }
                    var url = 'api/MstCAD/URNTag';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                        $modalInstance.close('closed');
                        activate();
                    });
                    }
                };
            }

        }
        $scope.reassign_application = function (val) {
            $location.url('app/MstCADReassignApplication?application_gid=' + val);
        }

        $scope.assign_application = function (val) {
            $location.url('app/MstCADGroupProcessAssign?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.limit_management = function (val, customer_urn) {
            $location.url('app/MstLimitManagementView?application_gid=' + val + '&customer_urn=' + customer_urn + '&lspage=MstCadUrnGrouping');
        }

        $scope.npa_tag_list = function () {
            $location.url('app/MstCadUrnAcceptedCustomersNPAtag');
        }

        $scope.legal_tag_list = function () {
            $location.url('app/MstCadUrnAcceptedCustomersLegaltag');
        }
    }
})();
