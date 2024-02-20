(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedCallViewController', MstMarketingAssignedCallViewController);

    MstMarketingAssignedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingAssignedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedCallViewController';

        //$scope.marketingcall_gid = $location.search().marketingcall_gid;
        //var marketingcall_gid = $scope.marketingcall_gid;
        //$scope.lspage = $location.search().lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;


        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.completedshow = false;

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

            if (lspage == 'TransferCall') {
                $scope.transfershow = true;
                $scope.followupshow = false;
                $scope.completedshow = false;
            }
            else if (lspage == 'FollowUpCall') {
                $scope.followupshow = true;
                $scope.transfershow = false;
                $scope.completedshow = false;
            }
            else if (lspage == 'CompletedCall') {
                $scope.completedshow = true;
                $scope.transfershow = false;
                $scope.followupshow = false;
            }
            else {

            }

            var params = {
                marketingcall_gid: marketingcall_gid
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
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.origination = resp.data.origination,
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


            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;

                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lefilename = resp.data.filename;
                $scope.lefilepath = resp.data.filepath;
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
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1,val2);
        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }        
        $scope.milletdocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }        
        $scope.enquirydocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
             DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.add_leadstatus = function () {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;


                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {

                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';


                lsloanproduct_name = $('#loanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#loansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }

            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
                //var  temp = $scope.ibcallleadstatus_list ;
                //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

        //

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Closed') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Converted') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }


        $scope.Back = function () {
            if (lspage == 'TransferCall') {
                $location.url('app/MstMarketingTransferCallSummary');
            }
            else if (lspage == 'FollowUpCall') {
                $state.go('app.MstMarketingFollowUpCallSummary');
            }
            else if (lspage == 'CompletedCall') {
                $state.go('app.MstMarketingCompletedCallSummary');
            }
            else {
                
            }
        }

        
    }
})();
