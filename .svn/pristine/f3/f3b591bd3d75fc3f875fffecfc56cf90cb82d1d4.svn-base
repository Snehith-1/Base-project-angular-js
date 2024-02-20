(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedInboundCallViewController', MstAssignedInboundCallViewController);

    MstAssignedInboundCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstAssignedInboundCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedInboundCallViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.inboundcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;


        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.closedshow = false;
        $scope.RejectedShow = false;

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

            if (lspage == 'ClosedInboundCall') {
                $scope.closedshow = true;
                $scope.followupshow = false;
                $scope.completedshow = false;
                $scope.RejectedShow = false;
            }
            else if (lspage == 'FollowUpInboundCall') {
                $scope.followupshow = true;
                $scope.completedshow = false;
                $scope.closedshow = false;
                $scope.RejectedShow = false;
            }
            else if (lspage == 'CompletedInboundCall') {
                $scope.completedshow = true;
                $scope.closedshow = false;
                $scope.followupshow = false;
                $scope.RejectedShow = false;
            }
            else if (lspage == 'RejectedInboundCall') {
                $scope.RejectedShow = true;
                $scope.closedshow = false;
                $scope.followupshow = false;
                $scope.completedshow = false;
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
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txtclosed_date = resp.data.closed_date,
                $scope.txtclosed_by = resp.data.closed_by,
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                 $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
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

                 $scope.downloadallcall = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }
        
                }
                $scope.downloadallproof = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }
        
                }
        
        
        $scope.Back = function () {
            if (lspage == 'ClosedInboundCall') {
                $location.url('app/MstClosedInboundCallSummary');
            }
            else if (lspage == 'FollowUpInboundCall') {
                $state.go('app.MstFollowUpInboundCallSummary');
            }
            else if (lspage == 'CompletedInboundCall') {
                $state.go('app.MstCompletedInboundCallSummary');
            }
            else if (lspage == 'RejectedInboundCall') {
                $state.go('app.MstRejectedInboundCallSummary');
            }
            else {

            }
        }
    }
})();
