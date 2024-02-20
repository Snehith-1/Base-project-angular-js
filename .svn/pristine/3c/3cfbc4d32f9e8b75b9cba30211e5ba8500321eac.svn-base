(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignEditController', FndTrnCampaignEditController);

    FndTrnCampaignEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCampaignEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignEditController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var lsQuestionnarie_gid;
        var lscategory_gid;
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

            $scope.cboCampaign_apr == '';

            var params = {
                campaign_gid: $scope.campaign_gid
            }
            var url = 'api/FndTrnCampaign/GetCampaigntype';
            SocketService.get(url).then(function (resp) {
                $scope.campaigntype_list = resp.data.campaigntype_list;
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

           

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboemployee_editlist = resp.data.assignee;
                $scope.employees = resp.data.employees;
                $scope.cboemployee_edit = [];
                if (resp.data.employees != null) {
                    var count = resp.data.employees.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cboemployee_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.employees[i].employee_gid);
                       // var indexs = $scope.cboemployee_editlist.findIndex(x => x.employee_gid === resp.data.employees[i].employee_gid);
                        $scope.cboemployee_edit.push($scope.cboemployee_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }  
                if (resp.data.campaign_cost != null) {
                    $scope.campaign = resp.data.campaign_cost;
                    var input =  $scope.campaign;
                    var str = input.replace(/,/g, '');
                    var lswords_annualincome = cmnfunctionService.fnConvertNumbertoWord(str);
                    document.getElementById('words_campaign_cost').innerHTML = lswords_annualincome;

                   // campaign_cost_change(campaign);
                }            
                unlockUI();
            });
               var params = {
                   campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
                }
                var url = 'api/FndTrnCampaign/GetSingleformEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.singleform_list = resp.data.singleform_list;
                    if (resp.data.length == null) {
                        $scope.Qtype_disabled = false;
                    }
                    else { $scope.Qtype_disabled = true; }
                    
                });
            
    
                var params = {
                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
                }
                var url = 'api/FndTrnCampaign/GetMultipleformEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.multipleform_list = resp.data.multipleform_list;
                    if (resp.data.length == null) {
                        $scope.Qtype_disabled = false;
                    }
                    else { $scope.Qtype_disabled = true; }

                });
                var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
                });
        }
       
        $scope.txtcampaign_cost_change = function () {
            var input = document.getElementById('campaign_cost').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualincome = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.campaign_cost = "";
            }
            else {
                $scope.campaign_cost = output;
                document.getElementById('words_campaign_cost').innerHTML = lswords_annualincome;
            }
        }
        $scope.pastdatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/PastDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }

        $scope.checkErr = function (startDate, endDate) {
            $scope.errMessage = '';
            var curDate = new Date();

            if (new Date(startDate) > new Date(endDate)) {
                $scope.errMessage = 'End Date should be greater than start date';
                $scope.txtstart_date = '';
                $scope.txtend_date = ''
                return false;
            }
            //if (new Date(startDate) < curDate) {
            //    $scope.errMessage = 'Start date should not be before today.';
            //    return false;
            //}
        };
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
        $scope.campaign_update = function () {
            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnCampaign/GetSingleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;

            });


            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnCampaign/GetMultipleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleform_list = resp.data.multipleform_list;

            });

            //if (($scope.singleform_list == null) || ($scope.multipleform_list == null)) {
             if (($scope.singleform_list !== null) && ($scope.multipleform_list == !null) || ($scope.singleform_list == null) && ($scope.multipleform_list == null) ) {
                Notify.alert('Select Single/Multiple Questionnarie', 'warning');
            }
            else {
                var params = {

                    campagin_name: $scope.campaign_name,
                    campaigntype_gid: $scope.cboCampaign_type,
                    customer_gid: $scope.cboCustomer,
                    contact_name: $scope.contact_name,
                    contact_mobile: $scope.contact_mobile,
                    contact_email: $scope.contact_email,
                    assignee: $scope.cboemployee_edit,
                    campaign_approver: $scope.cboCampaign_apr,
                    campaign_cost: $scope.campaign_cost,
                    start_date: $scope.start_date,
                    end_date: $scope.end_date,
                    assesment_date: $scope.assesment_date,
                    osAssesment_date: $scope.os_assesment_date,
                    loan_availed: $scope.loan_availed,
                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

                }
                var url = 'api/FndTrnCampaign/campaignEditUpdate';
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
        $scope.query_close = function (campaignraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        campaignraisequery_gid: campaignraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
                    }
                    var url = 'api/FndTrnCampaign/PostCampaignresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.view_responsequerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/responsequeryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        
        $scope.Back = function () {
            $state.go('app.FndTrnCampaignSummary');
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
                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                }
                var url = 'api/FndTrnCampaign/PostSingleformEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                                             
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        GetSsummary(cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid);
                        $scope.cboSquestionnarie_name.questionnarie_name = "";
                        lscampaign_gid = resp.data.campaign_gid;
                        lscategory_gid = resp.data.category_gid;
                        dropdown_list(lscampaign_gid, lscategory_gid);
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

        function dropdown_list(lscampaign_gid, lscategorytype_gid) {
            var params = {
                campaign_gid: lscampaign_gid,
                categorytype_gid: lscategory_gid,
            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCampaignQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.questionnarielist = resp.data.questionnarie_list;
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
                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

                }
                var url = 'api/FndTrnCampaign/PostMultipleformEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        lsMcampaign_gid = resp.data.campaign_gid;
                        lscampaign_gid = resp.data.campaign_gid;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        GetMsummary(cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid);
                        $scope.cboMquestionnarie_name.questionnarie_name = "";
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
                    GetSsummary(cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid);
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
                    GetMsummary(cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid);
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
        function GetSsummary(lscampaign_gid) {
            var params = {
                campaign_gid: lscampaign_gid,
            }
            var url = 'api/FndTrnCampaign/GetSingleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
                
            });
        }
        function GetMsummary(lscampaign_gid) {
            var params = {
                campaign_gid: lscampaign_gid,
            }
            var url = 'api/FndTrnCampaign/GetMultipleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleform_list = resp.data.multipleform_list;
               
            });
        }
       
        $scope.getdetails = function (cboCustomer) {
            var params = {
                customer_gid: $scope.cboCustomer,

            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetCustomerdtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.contact_name = resp.data.contactperson_fn;
                $scope.contact_mobile = resp.data.mobile_no;
                $scope.contact_email = resp.data.email_address;
            });

            unlockUI();
        }
        
       
    }

})();
