﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedFollowupLeadsController', MstMarketingAssignedFollowupLeadsController);

    MstMarketingAssignedFollowupLeadsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstMarketingAssignedFollowupLeadsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedFollowupLeadsController';

        $scope.marketingcall_gid = $location.search().marketingcall_gid;
        var marketingcall_gid = $scope.marketingcall_gid;
        $scope.lspage = $location.search().lspage;
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
                  $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
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

        //for close and convert call
          
        var url = 'api/Marketing/GetEntity';
        SocketService.get(url).then(function (resp) {
            $scope.entity_list = resp.data.inboundentity_list;
        });
        var url = 'api/Marketing/GetLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        leadstatus_list();
   

    }//activate ends here

 //
        //lead status Multiple Add
        $scope.add_leadstatus = function ()
        {
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

           
        $scope.changeentitiy = function (cboentity) {
            for (var i = 0; i < $scope.entity_list.length; i++)
            {
                if (cboentity == $scope.entity_list[i].entity_gid)
                $scope.entityselect = $scope.entity_list[i].entity_name
            }
        }
        $scope.Back = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.FollowupSubmit = function () {   
          
            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
               Notify.alert('Kindly Fill Closed', 'warning')
            }
           
            
            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
               var params = {
                   marketingcall_gid: marketingcall_gid,
                   followup_date: $scope.txtfollowup_date,
                   followup_time: $scope.txtfollowup_time,
                   closed: $scope.cboclosed,
                   closure_status: $scope.cboclosurestatus,
                 
                   followup_remarks: $scope.txtfollowup_remarks
               }
               var url = 'api/Marketing/PostCompletedCall';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $location.url("app/MstMarketingFollowUpCallSummary");
                   }
                   else {
                       Notify.alert(resp.data.message, {
                           status: 'warning',
                           pos: 'top-center',
                           timeout: 3000
                       });
                   }
               });
           }
           }

        //
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
        $scope.Submit = function () {

            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
                Notify.alert('Kindly Fill Closed', 'warning')
            }


            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
                var params = {
                    marketingcall_gid: marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    closed: $scope.cboclosed,
                    closure_status: $scope.cboclosurestatus,

                    followup_remarks: $scope.txtfollowup_remarks
                }
                var url = 'api/Marketing/PostFollowupLeadCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingWorkInprogressCallSummary");
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
       
         function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
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
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    marketingcall_gid:marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    followup_status: $scope.cbofollowup,
                    followup_remarks: $scope.cboremarks,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUpMg';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        followup_list();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    $scope.cbofollowup = '';
                    $scope.cboremarks = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    followup_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_list();
            });
        }
        function followup_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallMyFollowUpList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }
        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);
                    $scope.cboeditfollowup = resp.data.followup_status;
                    $scope.cboremarks = resp.data.followup_remarks;
                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }
                  
                    followup_list();
                });


                $scope.editfollowup_change = function (cboeditfollowup) {
                    if (cboeditfollowup == 'Hot') {
                        $scope.cboremarks = 'Hot:will be closed in 1 month';
                    }
                    else if (cboeditfollowup == 'Warm') {
                        $scope.cboremarks = 'Warm:will be closed in 3 month';
                    }
                    else if (cboeditfollowup == 'Cold') {
                        $scope.cboremarks = 'Cold: will be closed in 6 month';
                    }
                    else if (cboeditfollowup == 'Others') {
                        $scope.cboremarks = 'Unqualified';
                    }
                    else {
                        $scope.cboremarks = '';
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        followup_status: $scope.cboeditfollowup,
                        followup_remarks: $scope.cboremarks,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            followup_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                      //  followup_templist();
                        followup_list();

                    });

                    $modalInstance.close('closed');

                }
            }
            followup_list();
        }
    
        $scope.followup_change = function (cbofollowup) {
            if (cbofollowup == 'Hot') {
                $scope.cboremarks = 'Hot:will be closed in 1 month';
            }
            else if (cbofollowup == 'Warm') {
                $scope.cboremarks = 'Warm:will be closed in 3 month';
            }
            else if (cbofollowup == 'Cold') {
                $scope.cboremarks = 'Cold: will be closed in 6 month';
            }
            else if (cbofollowup == 'Others') {
                $scope.cboremarks = 'Unqualified';
            }
            else {
                $scope.cboremarks = '';
            }
        }


    }
})();