(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalController', FndTrnMyCampaignApprovalController);

    FndTrnMyCampaignApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function FndTrnMyCampaignApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalController';
    
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        
        
        var lsQuestionnarie_gid;

        /* ----------------------- RADIO TEXT EDITOR ---------------------------- */
        $scope.answer = false;



        activate();
     

        function activate() {
            $scope.form_submit = false;
            $scope.form_update = false;

            $scope.selectOptions = '';
            $scope.choices = '';
          
            var params = {

                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetCampaignDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.choices = resp.data.campaign_details;
                $scope.campaign_name = resp.data.campaign_name;
                angular.forEach($scope.choices, function (value, key) {
                    $scope.campaign_name = value.campaign_name;
                    $scope.campaignrefno = value.campaignrefno;
                    $scope.campaign_type = value.campaign_type;
                    $scope.customer_name = value.customer_name;
                    $scope.contactperson_fn = value.contactperson_fn;
                    $scope.contactperson_mobile = value.contactperson_mobile;
                    $scope.contactperson_email = value.contactperson_email;
                    $scope.start_date = value.start_date;
                    $scope.end_date = value.end_date;
                    $scope.assesment_date = value.assesment_date;
                });


                //$scope.selectOptions = resp.data.answer_desc;
                //$scope.type = resp.data.answer_type
                angular.forEach($scope.choices, function (value, key) {

                    if (value.answer_type == "Text") {

                    }
                    else if (value.answer_type == "List") {
                        var list_desc = value.answer_desc.split(',');
                        $scope.selectOptions = list_desc;

                        console.log($scope.selectOptions);
                    }
                    else if (value.answer_type == "Number") {

                    }
                    else if (value.answer_type == "Radio_Button") {

                        $scope.radioOptions = value.answerdesc_list;
                        console.log($scope.radioOptions);

                    }





                });



            });

            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignSingle';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    if (resp.data.lsFlag == 'Y') {
                        $scope.form_submit = true;
                        $scope.form_details = true;
                        var params = {

                            campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                        }
                        var url = 'api/FndTrnMyCampaignSummary/GetSingleCampaignSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.singleformanswer_list = resp.data.campaign_list;
                            $scope.form_summary = false;
                        });
                    }
                    else {
                        $scope.form_submit = false;
                        $scope.form_details = false;
                        $scope.form_summary = true;


                    }

                }
                $scope.singleformanswer_list = resp.data.singleformanswer_list;
            });
            $scope.stripAddr = function (value) {
                return value;
            }
            //var params = {

            //    campaign_gid: $location.search().lscampaign_gid,
            //}
            //var url = 'api/FndTrnMyCampaignSummary/GetMycampaignMultiple';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.multipleformanswer_list = resp.data.multipleformanswer_list;

            //});
            //var params = {

            //    campaign_gid: $location.search().lscampaign_gid,
            //}
            //var url = 'api/FndTrnMyCampaignSummary/GetMycampaignTeamActivity';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.multipleformTeamanswer_list = resp.data.multipleformTeamanswer_list;

            //});
            var params = {

                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleCampaignSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleformanswer_list = resp.data.campaign_list;

            });
            GetMycampaignMultiple();
            GetMycampaignTeamActivity();
            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignApprovalRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });
        }
        function GetMycampaignMultiple() {
            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignMultiple';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);
                    $scope.invisible = true;

                }
                else {
                    $scope.SampleDynamicTabledata = "";
                    $scope.invisible = false;


                }
            });
        }
        function GetMycampaignTeamActivity() {
            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignTeamActivity';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata1 = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata1 = angular.copy($scope.SampleDynamicdata1);

                    $scope.SampleDynamicTable1 = angular.copy($scope.SampleDynamicTabledata1);
                    $scope.invisibleTeam = true;

                }
                else {
                    $scope.SampleDynamicTabledata1 = "";
                    $scope.invisibleTeam = false;


                }
            });
        }
        $scope.approver_update = function () {
            var params = {
                mycampaignapproval_remarks: $scope.remarks,
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

            }
            var url = 'api/FndTrnMyCampaignSummary/MyCampaignApprovedSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnMyCampaignApprovalPending');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }


        $scope.rejected_update = function () {
            var params = {
                mycampaignapproval_remarks: $scope.remarks,
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

            }
            var url = 'api/FndTrnMyCampaignSummary/MyCampaignRejectSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnMyCampaignApprovalPending');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }
      
        $scope.myraisequery = function (campaign_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    campaign_gid: campaign_gid
                }

                var url = 'api/FndTrnMyCampaignSummary/GetCampaignManager';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaignmanager_list = resp.data.campaignmanager_list;
                    unlockUI();
                });
                $scope.submit = function () {
                    var lsmanager_gid = '';
                    var lsmanager_name = '';
                    if ($scope.cboemployee_name != undefined || $scope.cboemployee_name != null) {
                        lsmanager_gid = $scope.cboemployee_name.manager_gid;
                        lsmanager_name = $scope.cboemployee_name.manager_name;
                    }

                    var params = {
                        campaign_gid: campaign_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,                       
                        manager_gid: lsmanager_gid,
                        manager_name: lsmanager_name,
                    }
                    var url = 'api/FndTrnMyCampaignSummary/PostMyCampaignraisequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //activate();
                            address_list(campaign_gid);
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }


        function address_list(campaign_gid) {
            var params = {
                campaign_gid: campaign_gid,

            }

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });
        }
        $scope.view_myquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/myqueryDescriptionView.html',
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

        $scope.showPopover = function (campaignmanager2employee_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    campaignmanager2employee_gid: campaignmanager2employee_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetManagerEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.manager_name = resp.data.manager_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }

        $scope.AddMultiple = function () {
            var lsstatus, lsmessage;
                var modalInstance = $modal.open({
                    templateUrl: '/myModalContent.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
               
               
                    var params = {

                        campaign_gid:  cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
                    }
                    var url = 'api/FndTrnMyCampaignSummary/GetCampaignMultipleDetails';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.multichoices = resp.data.multi_campaign_details;
                        $scope.campaign_name = resp.data.campaign_name;
                        angular.forEach($scope.multichoices, function (value, key) {
                            $scope.campaign_name = value.campaign_name;
                            $scope.campaignrefno = value.campaignrefno;
                            $scope.campaign_type = value.campaign_type;
                            $scope.customer_name = value.customer_name;
                            $scope.contactperson_fn = value.contactperson_fn;
                            $scope.contactperson_mobile = value.contactperson_mobile;
                            $scope.contactperson_email = value.contactperson_email;
                            $scope.start_date = value.start_date;
                            $scope.end_date = value.end_date;
                            $scope.assesment_date = value.assesment_date;
                        });


                        //$scope.selectOptions = resp.data.answer_desc;
                        //$scope.type = resp.data.answer_type
                        angular.forEach($scope.multichoices, function (value, key) {

                            if (value.answer_type == "Text") {

                            }
                            else if (value.answer_type == "List") {
                                var list_desc = value.answer_desc.split(',');
                                $scope.selectOptions = list_desc;

                                console.log($scope.selectOptions);
                            }
                            else if (value.answer_type == "Number") {

                            }
                            else if (value.answer_type == "Radio_Button") {

                                $scope.multiradioOptions = value.answerdesc_list;
                                console.log($scope.multiradioOptions);

                            }



                        });



                    });
                    unlockUI();
                    $scope.MultipleSubmit = function () {
                        var params;
                     
                        var setmultipleid = Math.floor(Math.pow(10, 6 - 1) + Math.random() * (Math.pow(10, 6) - Math.pow(10, 6 - 1) - 1));
                        angular.forEach($scope.multichoices, function (value, key) {
                           
                            if (value.answer_type == "Text") {
                                params = {
                                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                    questionnarie_gid: value.questionnarie_gid,
                                    questionnarie_name: value.question,
                                    questionnarie_type: value.answer_type,
                                    questionnarie_answer: value.name,
                                    form_type: setmultipleid,
                                }
                                var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleSubmit';
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {
                                        lsstatus = 'success';
                                        lsmessage = resp.data.message;
                                        setTimeout(1000);
                                    }
                                });
                             
                            }

                            else if (value.answer_type == "Number") {
                                params = {
                                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                    questionnarie_gid: value.questionnarie_gid,
                                    questionnarie_name: value.question,
                                    questionnarie_type: value.answer_type,
                                    questionnarie_answer: value.name,
                                    form_type: setmultipleid,

                                }
                              
                                var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleSubmit';
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {
                                        lsstatus = 'success';
                                        lsmessage = resp.data.message;
                                        setTimeout(1000);
                                    }
                                });
                            }
                        });


                        $modalInstance.close('closed');
                        activate();
                    }
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            
        }
        $scope.add = function () {
            var dataObj = { l: '', v: '' };
            $scope.inputs.push(dataObj);
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }
        $scope.viewcampaign = function () {
            var lsstatus, lsmessage;
            var modalInstance = $modal.open({
                templateUrl: '/ViewSingleform.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {

                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                }
                var url = 'api/FndTrnMyCampaignSummary/GetMycampaignSingle';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.singlechoices = resp.data.campaign_details;

                    angular.forEach($scope.singlechoices, function (value, key) {

                        if (value.answer_type == "Text") {

                        }
                        else if (value.answer_type == "List") {
                            var list_desc = value.answer_desc.split(',');
                            $scope.selectOptions = list_desc;

                        }
                        else if (value.answer_type == "Number") {

                        }
                        else if (value.answer_type == "Radio_Button") {

                            $scope.radioOptions = value.answerdesc_list;


                        }



                    });



                });

                unlockUI();
                $scope.singleUpdate = function () {
                    var params;

                    lockUI();
                    angular.forEach($scope.singlechoices, function (value, key) {

                        if (value.answer_type == "Text") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.name,

                            }
                        }
                        else if (value.answer_type == "List") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.type,

                            }

                        }
                        else if (value.answer_type == "Number") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.name,

                            }

                        }
                        else if (value.answer_type == "Radio_Button") {

                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.singleRadio,

                            }


                        }

                    });
                    unlockUI();


                    $modalInstance.close('closed');
                    if (lsstatus == 'success') {

                        Notify.alert(lsmessage, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(lsmessage, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    activate();
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }
        $scope.viewmultiplecampaign = function (reference_Id) {
            var lsstatus, lsmessage;
            var modalInstance = $modal.open({
                templateUrl: '/ViewMultiple.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {

                    campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                    reference_gid: reference_Id,
                }
                var url = 'api/FndTrnMyCampaignSummary/GeteditMycampaignMultiple';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.editmultichoices = resp.data.multi_campaign_details;

                    angular.forEach($scope.singlechoices, function (value, key) {

                        if (value.answer_type == "Text") {
                        }
                        else if (value.answer_type == "Number") {
                        }
                    });



                });

                unlockUI();
                $scope.MultipleUpdate = function () {
                    var params;

                    lockUI();
                    angular.forEach($scope.editmultichoices, function (value, key) {

                        if (value.answer_type == "Text") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignmultiple_gid: value.mycampaignmultiple_gid,
                                questionnarie_answer: value.name,

                            }
                            //var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleUpdate';
                            //SocketService.post(url, params).then(function (resp) {
                            //    if (resp.data.status == true) {
                            //        lsstatus = 'success';
                            //        lsmessage = resp.data.message;
                            //        setTimeout(1000);
                            //    }
                            //});

                        }

                        else if (value.answer_type == "Number") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignmultiple_gid: value.mycampaignmultiple_gid,
                                questionnarie_answer: value.name,

                            }
                            //var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleUpdate';
                            //SocketService.post(url, params).then(function (resp) {
                            //    if (resp.data.status == true) {
                            //        lsstatus = 'success';
                            //        lsmessage = resp.data.message;
                            //        setTimeout(1000);
                            //    }
                            //});
                        }


                    });
                    unlockUI();


                    $modalInstance.close('closed');
                    if (lsstatus == 'success') {

                        Notify.alert(lsmessage, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(lsmessage, {
                            status: 'Warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    activate();
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }
        $scope.importexcel = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    auditcreation_gid: auditcreation_gid,
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                    DownloaddocumentService.Downloaddocument(val1, val2);

                    //var filename = "ImportExcelSample.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelSample.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }

                $scope.excelupload = function (val, val1, name) {

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelSample") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('auditcreation_gid', auditcreation_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmTrnSampling/ImportExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');
                            if (resp.data.status == true) {
                                defaultdynamic();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                //  $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }
        $scope.mycampaign_submit = function () {
            var params;
            var lsstatus, lsmessage;
          
            angular.forEach($scope.choices, function (value, key) {
                if (value.answer_type == "Text") {

                  
                    params = {
                        campaign_gid: $scope.campaign_gid,
                        questionnarie_gid: value.questionnarie_gid,
                        questionnarie_name: value.question,
                        questionnarie_type: value.answer_type,
                        questionnarie_answer: value.name,
                        form_type: 'S',
                    }
                   
                }
                else if (value.answer_type == "List") {
                    params = {
                        campaign_gid: $scope.campaign_gid,
                        questionnarie_gid: value.questionnarie_gid,
                        questionnarie_name: value.question,
                        questionnarie_type: value.answer_type,
                        questionnarie_answer: value.type,
                        form_type: 'S',
                    }
                    
                }
                else if (value.answer_type == "Number") {
                    params = {
                        campaign_gid: $scope.campaign_gid,
                        questionnarie_gid: value.questionnarie_gid,
                        questionnarie_name: value.question,
                        questionnarie_type: value.answer_type,
                        questionnarie_answer: value.name,
                        form_type: 'S',
                    }
                 
                 
                }
                else if (value.answer_type == "Radio_Button") {

                    params = {
                        campaign_gid: $scope.campaign_gid,
                        questionnarie_gid: value.questionnarie_gid,
                        questionnarie_name: value.question,
                        questionnarie_type: value.answer_type,
                        questionnarie_answer: value.selected,
                        form_type: 'S',
                    }
                  
                }
                var url = 'api/FndTrnMyCampaignSummary/MyCampaignSubmit';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    activate();
                });
         

           
              

            });

            params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

            }
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignSingle';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleformanswer_list = resp.data.singleformanswer_list;
            });
            //if (lsstatus = 'success') {
            //    params = {
            //        campaign_gid: $location.search().lscampaign_gid,

            //    }
            //    var url = 'api/FndTrnMyCampaignSummary/GetMycampaignSingle';
            //    SocketService.get(url).then(function (resp) {
            //        $scope.singleformanswer_list = resp.data.singleformanswer_list;
            //    });


            //}
            //else {
            //    Notify.alert(lsmessage, {
            //        status: 'info',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}


        }
      
    }



})();
