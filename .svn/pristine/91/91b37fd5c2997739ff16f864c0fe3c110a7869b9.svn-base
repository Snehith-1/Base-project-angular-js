(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallResponseController', MstMarketingCallResponseController);

    MstMarketingCallResponseController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingCallResponseController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallResponseController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        lockUI();
        activate();        
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                   marketingcall_gid:marketingcall_gid
               }
               var url = 'api/Marketing/GetMarketingCallAssignedView';
               SocketService.getparams(url, params).then(function (resp) {
                   $scope.txtticket_refid = resp.data.ticket_refid,
                   $scope.txtentity_name = resp.data.entity_name,
                   $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                   $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                   $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                   $scope.txtcallreceived_date = resp.data.callreceived_date,
                   $scope.txtcaller_name = resp.data.caller_name,
                   $scope.txtinternalreference_name = resp.data.internalreference_name,
                   $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                   $scope.txtoffice_landlineno = resp.data.office_landlineno,
                   $scope.txtcalltype_name = resp.data.calltype_name,
                   $scope.txtfunction_name = resp.data.function_name,
                   $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                   $scope.txtrequirement = resp.data.requirement,
                   $scope.txtenquiry_description = resp.data.enquiry_description,
                   $scope.txtcallclosure_status = resp.data.callclosure_status,
                   $scope.txtassignemployee_name = resp.data.assignemployee_name,
                   $scope.txtbase_location = resp.data.baselocation_name,
                   $scope.txtassign_date = resp.data.assign_date,
                   $scope.origination = resp.data.origination,
                   $scope.txttagemployee_name = resp.data.tagemployee_name,
                   $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                   $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                   $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                   $scope.txtacknowledgement_date = resp.data.acknowledge_date;
                   $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                   $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                   $scope.txtprimary_email = resp.data.primary_email,
                       $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                   $scope.txtleadrequire_name = resp.data.leadrequire_name,
                       $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                       $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                       $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                       $scope.txtbusiness_name = resp.data.business_name,
                       $scope.txtindustry_name = resp.data.industry_name,

                   unlockUI();
               });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }

        $scope.Back = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }

        $scope.Reject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectrequest.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        reject_remarks: $scope.txtreject_remarks
                    }
                    var url = 'api/Marketing/RejectMarketingCall';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            $state.go("app.MstMarketingMyAssignedCallSummary");
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                    });
                    $state.go("app.MstMarketingMyAssignedCallSummary");
                }
            }
        }
        $scope.acknowledge = function () {
            var url = 'api/Marketing/PostUpdateAck';
            lockUI();
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingMyAssignedCallSummary");
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingMyAssignedCallSummary");
                }
            });
        }
          $scope.closed_call = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                
                $scope.close_call = function () {                   
                    var params = {
                        marketingcall_gid: marketingcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/Marketing/MarketingCallAssignedClosed";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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

             $scope.documentviewermillet = function (val1, val2) {
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
        $scope.documentviewerenquiry = function (val1, val2) {
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
    }
})();