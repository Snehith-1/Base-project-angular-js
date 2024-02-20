(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignAddController', FndTrnCampaignAddController);

    FndTrnCampaignAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function FndTrnCampaignAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignAddController';
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var lscategory_gid;
        var lsMcategory_gid;
        var lsQuestionnarie_gid;
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
           

            var url = 'api/FndTrnCampaign/GetCampaigntype';
            SocketService.get(url).then(function (resp) {
                $scope.campaigntypelist = resp.data.campaigntype_list;
            });

            var url = 'api/FndTrnCampaign/GetCustomer';
            SocketService.get(url).then(function (resp) {
                $scope.customerlist = resp.data.customer_list;
                
            });
            var url = 'api/FndTrnCampaign/GetQuestionCategory';
            SocketService.get(url).then(function (resp) {
                $scope.categorytype_list = resp.data.category_list;

            });

            var url = 'api/FndTrnCampaign/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.assigneelist;
                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.assigneelist;
                unlockUI();
            });
        }
        $scope.pastdatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/PastDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                    $scope.txtAssesment_date = '';
                }
            });
        }

        $scope.checkErr = function (startDate, endDate) {
            $scope.errMessage = '';
            var curDate = new Date();

            if (new Date(startDate) > new Date(endDate)) {
                $scope.errMessage = 'End Date should be greater than start date';
                $scope.txtstart_date = '';
                 $scope.txtend_date= ''
                return false;
            }
            //if (new Date(startDate) < curDate) {
            //    $scope.errMessage = 'Start date should not be before today.';
            //    return false;
            //}
        };
        $scope.txtcampaign_cost_change = function () {
            var input = document.getElementById('txtcampaign_cost').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualincome = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcampaign_cost = "";
            }
            else {
                $scope.txtcampaign_cost = output;
                document.getElementById('words_campaign_cost').innerHTML = lswords_annualincome;
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
        $scope.getQuestionnarie = function (cboSCategory) {
            var params = {
                categorytype_gid: $scope.cboSCategory.categorytype_gid,
                campaign_gid: lscampaign_gid,
            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCampaignQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.questionnarielist = resp.data.questionnarie_list;
            });

            unlockUI();
        }
        $scope.getQuestionnarie1 = function (cboMCategory) {
            var params = {
                categorytype_gid: $scope.cboMCategory.categorytype_gid,

            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetMultipleListQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multiplequestionnarie_list = resp.data.multiplequestionnarie_list;
            });

            unlockUI();
        }
     
        $scope.Questionnarie_Sadd = function () {

            if (($scope.cboSCategory == undefined) || ($scope.cboSCategory == '-----Select Campaign Type-----')) {
                Notify.alert('Select Category Type', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (($scope.cboSquestionnarie_name == undefined) || ($scope.cboSquestionnarie_name == '-----Select Questionnarie Title-----')) {
                Notify.alert('Select Questionnarie title', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {


                var params = {
                    category_gid: $scope.cboSCategory.categorytype_gid,
                    questionnarie_name: $scope.cboSquestionnarie_name.questionnarie_name,
                    questionnarie_gid: $scope.cboSquestionnarie_name.questionnarie_gid,
                    campaign_gid: lscampaign_gid,
                }
                var url = 'api/FndTrnCampaign/PostSingleform';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        lsScampaign_gid = resp.data.campaign_gid;
                        lscampaign_gid = resp.data.campaign_gid;
                        lscategory_gid = resp.data.category_gid;
                        console.log('single' + lsScampaign_gid);
                        GetSsummary(lsScampaign_gid);
                        dropdown_list(lsScampaign_gid, lscategory_gid);
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
                  
                 
                });
            }
        }


        function dropdown_list(lsScampaign_gid, lscategorytype_gid) {
            var params = {
                campaign_gid: lsScampaign_gid,
                categorytype_gid: lscategory_gid,
            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCampaignQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.questionnarielist = resp.data.questionnarie_list;
            });

            unlockUI();
        }

        function dropdown_Mlist(lsMcampaign_gid, lscategorytype_gid) {
            var params = {
                campaign_gid: lsMcampaign_gid,
                categorytype_gid: lsMcategory_gid,
            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetMultipleSelectListQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multiplequestionnarie_list = resp.data.multiplequestionnarie_list;
            });

            unlockUI();
        }

        $scope.Questionnarie_Madd = function () {

            if (($scope.cboMCategory == undefined) || ($scope.cboMCategory == '-----Select Campaign Type-----')) {
                Notify.alert('Select Category Type', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (($scope.cboMquestionnarie_name == undefined) || ($scope.cboMquestionnarie_name == '-----Select Questionnarie Title-----')) {
                Notify.alert('Select Questionnarie title', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
               

                var params = {
                    category_gid: $scope.cboMCategory.categorytype_gid,
                    questionnarie_name: $scope.cboMquestionnarie_name.questionnarie_name,
                    questionnarie_gid: $scope.cboMquestionnarie_name.questionnarie_gid,
                    campaign_gid: lscampaign_gid,

                }
                var url = 'api/FndTrnCampaign/PostMultipleform';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        lsMcampaign_gid = resp.data.campaign_gid;
                        lscampaign_gid = resp.data.campaign_gid;
                        lsMcategory_gid = resp.data.category_gid;
                        GetMsummary(lsMcampaign_gid);
                        dropdown_Mlist(lsMcampaign_gid, lsMcategory_gid);
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


                });
            }
        }
        $scope.Questionnarie_Sdelete = function (campaigndtl_gid) {
            var params =
                {
                    campaigndtl_gid: campaigndtl_gid
                }
            console.log(params)
            var url = 'api/FndTrnCampaign/DeleteSingleform';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    lsScampaign_gid = resp.data.campaign_gid;
                  
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetSsummary(lsScampaign_gid);
                    dropdown_list(lsScampaign_gid, lscategory_gid);
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

        function dropdown_list(lsScampaign_gid, lscategorytype_gid) {
            var params = {
                campaign_gid: lsScampaign_gid,
                categorytype_gid: lscategory_gid,
            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCampaignQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.questionnarielist = resp.data.questionnarie_list;
            });

            unlockUI();
        }



        $scope.Questionnarie_Mdelete = function (campaigndtl_gid) {
            var params =
                {
                    campaigndtl_gid: campaigndtl_gid
                }
            console.log(params)
            var url = 'api/FndTrnCampaign/DeleteMultipleform';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    lsMcampaign_gid = resp.data.campaign_gid;
                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetMsummary(lsMcampaign_gid);
                    dropdown_Mlist(lsScampaign_gid, lsMcategory_gid);
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
        function GetSsummary(lsScampaign_gid) {
            var params = {
                campaign_gid: lsScampaign_gid,
            }
            var url = 'api/FndTrnCampaign/GetSingleform';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
                if (resp.data.length == null) {
                    $scope.Qtype_disabled = false;
                   
                }
              
                else { $scope.Qtype_disabled = true; }
            });
        }

       
        function GetMsummary(lsMcampaign_gid) {
            var params = {
                campaign_gid: lsMcampaign_gid,
            }
            var url = 'api/FndTrnCampaign/GetMultipleform';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleform_list = resp.data.multipleform_list;
                if (resp.data.length == null) {
                    $scope.Qtype_disabled = false;
                }
                else { $scope.Qtype_disabled = true; }
            });
        }
        $scope.Back = function () {
            $state.go('app.FndTrnCampaignSummary');
        }
        $scope.getdetails = function (cboCustomer) {
            var params = {
                customer_gid: $scope.cboCustomer.customer_gid,

            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCustomerdtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcontactperson = resp.data.contactperson_fn;
                $scope.txtcontactperson_mobile = resp.data.mobile_no;
                $scope.txtcontactperson_email = resp.data.email_address;
            });

            unlockUI();
        }
        
        $scope.campaign_submit = function () {
            //if (($scope.singleform_list == null) || ($scope.multipleform_list == null)) {
            if (($scope.singleform_list !== null) && ($scope.multipleform_list == !null) || ($scope.singleform_list == null) && ($scope.multipleform_list == null)) {
                Notify.alert('Select Single/Multiple Questionnarie', 'warning');
            }
            else {


                var params = {
                    campaign_name: $scope.txtcampaign_name,
                    campaign_type: $scope.cboCampaign_type.campaigntype_gid,
                    customer_gid: $scope.cboCustomer.customer_gid,
                    contactperson_fn: $scope.txtcontactperson,
                    contactperson_mobile: $scope.txtcontactperson_mobile,
                    contactperson_email: $scope.txtcontactperson_email,
                    assignee: $scope.cboCampaign_mgr,
                    campaign_apr: $scope.cboCampaign_apr.employee_gid,
                    campaign_cost: $scope.txtcampaign_cost,
                    start_date: $scope.txtstart_date,
                    end_date: $scope.txtend_date,
                    assesment_date: $scope.txtAssesment_date,
                    osAssesment_date: $scope.txtosAssesment_date,
                    loan_availed: $scope.txtloanavailed,
                    campaign_gid: lscampaign_gid,


                }
                var url = 'api/FndTrnCampaign/CampaignSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.FndTrnCampaignSummary');
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
        $scope.campaign_save = function () {


            var lscampaigntype_gid = '';
            var lscampaign_type = '';
            var lscustomer_gid = '';
            var lsemployee_gid = '';
            var lsmgremployee_gid = '';


            if ($scope.cboCampaign_type != undefined || $scope.cboCampaign_type != null) {
                lscampaigntype_gid = $scope.cboCampaign_type.campaigntype_gid;
            }
            if ($scope.cboCustomer != undefined || $scope.cboCustomer != null) {
                lscustomer_gid = $scope.cboCustomer.customer_gid;
            }
            if ($scope.cboCampaign_apr != undefined || $scope.cboCampaign_apr != null) {
                lsemployee_gid = $scope.cboCampaign_apr.employee_gid;
            }
            if ($scope.cboCampaign_mgr != undefined || $scope.cboCampaign_mgr != null) {
                lsmgremployee_gid = $scope.cboCampaign_mgr.employee_gid;
            }

            var params = {
                campaign_name: $scope.txtcampaign_name,
                //campaign_type: $scope.cboCampaign_type.campaigntype_gid,
                campaign_type: lscampaigntype_gid,
                //customer_gid: $scope.cboCustomer.customer_gid,
                customer_gid: lscustomer_gid,
                contactperson_fn: $scope.txtcontactperson,
                contactperson_mobile: $scope.txtcontactperson_mobile,
                contactperson_email: $scope.txtcontactperson_email,
                assignee: $scope.cboCampaign_mgr,
                //assignee: $scope.lsmgremployee_gid,
                //campaign_apr: $scope.cboCampaign_apr.employee_gid,
                campaign_apr: lsemployee_gid,
                campaign_cost: $scope.txtcampaign_cost,
                start_date: $scope.txtstart_date,
                end_date: $scope.txtend_date,
                assesment_date: $scope.txtAssesment_date,
                osAssesment_date: $scope.txtosAssesment_date,
                loan_availed: $scope.txtloanavailed,
                campaign_gid: lscampaign_gid,
                


            }
            var url = 'api/FndTrnCampaign/CampaignSave';
                 lockUI();
                 SocketService.post(url, params).then(function (resp) {
                     unlockUI();
                     if (resp.data.status == true) {

                         Notify.alert(resp.data.message, {
                             status: 'success',
                             pos: 'top-center',
                             timeout: 3000
                         });
                         $state.go('app.FndTrnCampaignSummary');
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

})();
