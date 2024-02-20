(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedCallViewController', MstAssignedCallViewController);

    MstAssignedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstAssignedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedCallViewController';

       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.inboundcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.completedshow = false;
        $scope.rejectedshow = false;



        
        activate();
        lockUI();
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
                $scope.rejectedshow = false;

            }
            else if (lspage == 'FollowUpCall') {
                $scope.followupshow = true;
                $scope.transfershow = false;
                $scope.completedshow = false;
                $scope.rejectedshow = false;

            }
            else if (lspage == 'CompletedCall') {
                $scope.completedshow = true;
                $scope.transfershow = false;
                $scope.followupshow = false;
                $scope.rejectedshow = false;
            }
            else if (lspage == 'RejectedCall') {
                $scope.completedshow = false;
                $scope.transfershow = false;
                $scope.followupshow = false;
                $scope.rejectedshow = true;

            }
            else {

            }

            var params = {
                inboundcall_gid: inboundcall_gid
            }
            var url = 'api/TeleCalling/GetIBCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.customer_type,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txttat_days = resp.data.tat_days,
                $scope.txttat_date = resp.data.tat_date,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list,
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list,
                $scope.inboundcallfollowup_list = resp.data.inboundcallfollowup_list,
                $scope.ibcalltaggedmember_list = resp.data.ibcalltaggedmember_list,
                $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list,
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
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list,
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.ibcallemail_list,
                unlockUI();
            });

            var url = 'api/TeleCalling/IBCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lpfilename = resp.data.filename;
                $scope.lpfilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/TeleCalling/IBCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });


        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
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

             $scope.documentviewerproof = function (val1, val2) {
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

        

        
        $scope.Back = function () {
            if (lspage == 'TransferCall') {
                //$location.url('app.MstTransferCallSummary');
                $state.go('app.MstTransferCallSummary');
            }
            else if (lspage == 'FollowUpCall') {
                $state.go('app.MstFollowUpCallSummary');
            }
            else if (lspage == 'CompletedCall') {
                $state.go('app.MstCompletedCallSummary');
            }
            else if (lspage == 'RejectedCall') {
                //(lspage == 'RejectedCall')
                $state.go('app.MstRejectedCallSummary');
            }
           
        }

        
    }
})();
