
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingClosedViewController', MstMarketingClosedViewController);

    MstMarketingClosedViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingClosedViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingClosedViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
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
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.txttat_hours = resp.data.tat_hours,
               $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.origination = resp.data.origination;

                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;

                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtclosed_date = resp.data.closed_date;
                $scope.txtclosed_by = resp.data.closed_by;
                $scope.txtrejected_date = resp.data.rejected_date;
                $scope.txtrejected_by = resp.data.rejected_by;
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtclosed = resp.data.closed;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
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

        $scope.download_allmillet = function (val1,val2) {
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
       $scope.download_allupload = function (val1,val2) {
        for (var i = 0; i < val2.length; i++) {
           //  console.log(array[i]);
           DownloaddocumentService.Downloaddocument(val1, val2[i]);
       }
   }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerupload = function (val1, val2) {
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
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
     //   $scope.Back = function(){
       //     $location.url("app/MstMarketingClosedCall");
     //   }
        $scope.Back = function () {
            if (lspage == 'MarketingCloseAddLead') {
                $location.url('app/MstMarketingClosedCall');
            }
            else if (lspage == 'MarketingCloseMyLead') {
                $state.go('app.MstMarketingMyleadsClosedCall');
            }           
        }
    }
})();