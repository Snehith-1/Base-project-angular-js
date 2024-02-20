(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignEditController', FndTrnMyCampaignEditController);

    FndTrnMyCampaignEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignEditController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        $scope.Questionnarie_gid = cmnfunctionService.decryptURL($location.search().hash).lsQuestionnarie_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var invisible = false;
        var invisibleTeam = false;
        var lsQuestionnarie_gid;
       

        /* ----------------------- RADIO TEXT EDITOR ---------------------------- */
        $scope.answer = false;
        var ShowSinglefrm = false;


        activate();
     

        function activate() {
            $scope.form_submit = false;
            $scope.form_update = false;
            $scope.form_summary = false;

            $scope.selectOptions = '';
            $scope.choices = '';    
            $scope.SampleDynamicTabledata1 = "";
            $scope.invisibleTeam = false;
            $scope.SampleDynamicTabledata = "";
            var params = {

                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetCampaignDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
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
                    $scope.campaign_status = value.campaign_status;
                });

                
                //$scope.selectOptions = resp.data.answer_desc;
                //$scope.type = resp.data.answer_type
                angular.forEach($scope.choices, function (value, key) {
                    var list_desc = '';
                    if (value.answer_type == "Text") {

                    }
                    else if (value.answer_type == "List") {
                         list_desc = value.answer_desc.split(',');
                     //   $scope.selectOptions = list_desc;
                        value.answer_desc = list_desc;
                        console.log(value.answer_desc);
                    }
                    else if (value.answer_type == "Number") {

                    }
                    else if ((value.answer_type == "Radio Button") || (value.answer_type == "Radio_Button")) {

                        value.radioOptions = value.answerdesc_list;
                    
                    }
                });
             

            });
            lockUI();
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignSingle';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    if (resp.data.lsFlag != 'Nothing')
                    {
                        $scope.ShowSinglefrm = true;
                        if (resp.data.lsFlag == 'Y') {
                            $scope.form_submit = true;
                            $scope.form_details = true;
                            $scope.form_summary = false;
                            var params = {

                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                            }
                            var url = 'api/FndTrnMyCampaignSummary/GetSingleCampaignSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.singleformanswer_list = resp.data.campaign_list;

                            });
                        }
                        else {
                            $scope.form_submit = false;
                            $scope.form_details = false;
                            $scope.form_summary = true;


                        }
                    }
                    else {
                        $scope.ShowSinglefrm = false;


                    }

                }
                
              // $scope.singleformanswer_list = resp.data.singleformanswer_list;
            });
            GetMycampaignMultiple();
            GetMycampaignTeamActivity();
           

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });
        }
        
        $scope.editmultipleform = function (reference_Id) {
            var lsstatus, lsmessage;
            var modalInstance = $modal.open({
                templateUrl: '/EditMultiple.html',
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
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
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
                                mycampaignmultiple_gid: value.mycampaignmultiple_gid,
                                questionnarie_answer: value.name,

                            }
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignMultipleUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    lsstatus = 'success';
                                    lsmessage = resp.data.message;
                                    setTimeout(1000);
                                }
                            });
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
                            status: 'warning',
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
        $scope.editcampaign = function () {
            var lsstatus, lsmessage;
            var modalInstance = $modal.open({
                templateUrl: '/EditSingleform.html',
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
                            //$scope.selectOptions = list_desc;
                            value.answer_desc = list_desc;
                       
                        }
                        else if (value.answer_type == "Number") {
                         
                        }
                        else if ((value.answer_type == "Radio_Button") || (value.answer_type == "Radio Button")) {

                            $scope.radioOptions = value.answerdesc_list;

                        }



                    });



                });

                unlockUI();
                $scope.onTaskSelect = function (item, mycampaignsingle_gid) {
                  
                    console.log(item);    
                    angular.forEach($scope.singlechoices, function (value, key) {
                        if ((value.answer_type == "Radio_Button") || (value.answer_type == "Radio Button")) {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: mycampaignsingle_gid,
                                questionnarie_answer: item.name,

                            }
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    lsstatus = 'success';
                                    lsmessage = resp.data.message;
                                    setTimeout(1000);
                                }
                            });
                        }
                    });
                };
                $scope.singleUpdate = function () {
                    var params;

                    lockUI();
                    angular.forEach($scope.singlechoices, function (value, key) {

                        if (value.answer_type == "Text") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer:  value.name,
                               
                            }
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    lsstatus = 'success';
                                    lsmessage = resp.data.message;
                                    setTimeout(1000);
                                }
                            });

                        }
                        else if (value.answer_type == "List") {
                            params = {
                                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.type,

                            }
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
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
                                mycampaignsingle_gid: value.mycampaignsingle_gid,
                                questionnarie_answer: value.name,

                            }
                            var url = 'api/FndTrnMyCampaignSummary/MyCampaignUpdate';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    lsstatus = 'success';
                                    lsmessage = resp.data.message;
                                    setTimeout(1000);
                                }
                            });
                        }
                        //else if (value.answer_type == "Radio_Button") {
                            
                        //    params = {
                        //        campaign_gid: $location.search().lscampaign_gid,
                        //        mycampaignsingle_gid: value.mycampaignsingle_gid,
                        //        questionnarie_answer: value.SelectedId,

                        //    }
                        //    var url = 'api/FndTrnMyCampaignSummary/MyCampaignUpdate';
                        //    SocketService.post(url, params).then(function (resp) {
                        //        if (resp.data.status == true) {
                        //            lsstatus = 'success';
                        //            lsmessage = resp.data.message;
                        //            setTimeout(1000);
                        //        }
                        //    });

                        //}

                    });
                   
                    
                    if (lsstatus == 'success') {

                        Notify.alert(lsmessage, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(lsmessage, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                   
           
                    $modalInstance.close('closed');
                    unlockUI();
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
            }

        }
        $scope.onSingleTaskSelect = function ( itemname, questionnariegid,question, answer_type) {

            console.log(itemname);
                    console.log(itemname);
                    console.log(questionnariegid);
            //angular.forEach($scope.choices, function (value, key) {
                
            //    //if ((value.answer_type == "Radio_Button") || (value.answer_type == "Radio Button")) {
            //    if ( questionnariegid = value.questionnarie_gid)
            //    {
            //        console.log(itemname);
            //        console.log(itemname);
            //        console.log(questionnariegid);
                        var params = {
                            campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
                            questionnarie_gid: questionnariegid,
                            questionnarie_name: question,
                            questionnarie_type: answer_type,
                            questionnarie_answer: itemname,
                            form_type: 'S',
                        }


                        var url = 'api/FndTrnMyCampaignSummary/MyCampaignRadioSubmit';

                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                lsstatus = 'success';
                                lsmessage = resp.data.message;
                                setTimeout(1000);
                            }
                        });
            //        }
                  
            //  //  }
            //});
        };
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
                        else if ((value.answer_type == "Radio_Button") || (value.answer_type == "Radio Button")) {

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
                        else if ((value.answer_type == "Radio_Button") || (value.answer_type == "Radio Button")) {

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
                            status: 'warning',
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
                            status: 'warning',
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
        $scope.proceedtoapprove= function ()
        {
          var  params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,
            }
          var url = 'api/FndTrnMyCampaignSummary/CampaignFinalSubmit';
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
                activate();
            });
            $state.go('app.FndTrnMyCampaignOpen');

            
        }
       
        $scope.mycampaignquery_close = function (mycampaignraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryClose.html',
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
                        mycampaignraisequery_gid: mycampaignraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
                    }
                    var url = 'api/FndTrnMyCampaignSummary/PostMyCampaignresponsequery';
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
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryDescriptionView.html',
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
            $state.go('app.FndTrnMyCampaignOpen');
        }

        $scope.AddMultiple = function () {
            var lsstatus, lsmessage;
                var modalInstance = $modal.open({
                    templateUrl: '/myModalContent.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
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
                   
                        angular.forEach($scope.multichoices, function (value, key) {

                            if (value.answer_type == "Text") {

                            }
                            else if (value.answer_type == "Number") {

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
                        //GetMycampaignMultiple();
                    }
                   
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                }
            
        }

        
       
        
        $scope.importexcel = function () {      

            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
           
            var params = {             
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }

           
        
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {

                    var url = 'api/FndTrnMyCampaignSummary/ExcelTemplate';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            unlockUI();
                            var phyPath = resp.data.lspath;
                            var relPath = phyPath.split("EMS");
                            var relpath1 = relPath[1].replace("\\", "/");
                            var hosts = window.location.host;
                            var prefix = location.protocol + "//";
                            var str = prefix.concat(hosts, relpath1);
                            var link = document.createElement("a");
                            var name = resp.data.lsname.split('.');
                            link.download = name[0];
                            var uri = str;
                            link.href = uri;
                            link.click();

                          

                        }
                        else {

                            unlockUI();
                            Notify.alert('Error Occurred While DownLoading !', 'warning')
                        }

                    });

                  
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
                    else if (filePath.includes("Questionnaire") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                      
                        
                        var frm = new FormData();
                        var setmultipleid = Math.floor(Math.pow(10, 6 - 1) + Math.random() * (Math.pow(10, 6) - Math.pow(10, 6 - 1) - 1));
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('campaign_gid', cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid);
                       // frm.append('form_type', setmultipleid);
                       frm.append('project_flag', "RSK");
                      
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                      
                                           

                        //var params = {
                        //    campaign_gid: $location.search().lscampaign_gid,
                        //    form_type: setmultipleid
                           
                        //}
                        
                       
                        var url = 'api/FndTrnMyCampaignSummary/MyCampaignExcelSubmit';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                       //SocketService.post(url, params).then(function (resp) {
                            $modalInstance.close('closed');
                            if (resp.data.status == true) {
                                lsstatus = 'success';
                                lsmessage = resp.data.message;
                                setTimeout(1000);
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
                       
                               
                            activate();

                            
                            
                                
                        });
                            $("#fileimport").val('');
                       

                    }

                    
                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }

    


        $scope.stripAddr = function (value) {
            return value;
        }
        function GetMycampaignMultiple() {
            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignMultiple';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.SampleDynamicTabledata = "";
                    $scope.SampleDynamicdata = "";
                    $scope.SampleDynamicTable = "";
                    $scope.invisible = false;
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
            lockUI();
            var url = 'api/FndTrnMyCampaignSummary/GetMycampaignTeamActivity';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.SampleDynamicTabledata1 = "";
                    $scope.invisibleTeam = false;
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
        $scope.mycampaign_submit = function () {
            var params;
            var lsstatus, lsmessage,lsflag =  'N';
           
            angular.forEach($scope.choices, function (value, key) {
                if (value.answer_type == "Text") {
                    lsflag = 'Y';
                  
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
                    lsflag = 'Y';
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
                    lsflag = 'Y';
                    params = {
                        campaign_gid: $scope.campaign_gid,
                        questionnarie_gid: value.questionnarie_gid,
                        questionnarie_name: value.question,
                        questionnarie_type: value.answer_type,
                        questionnarie_answer: value.name,
                        form_type: 'S',
                    }
                 
                 
                }
              

                if (lsflag == 'Y'){
                    var url = 'api/FndTrnMyCampaignSummary/MyCampaignSubmit';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                         
                            lsstatus = 'success';
                            lsmessage = resp.data.message;
                            // setTimeout(3000);
                            console.log('submit' + lsmessage, lsstatus);
                        }
                        else {
                           
                            lsmessage = resp.data.message;
                            console.log('submitelse' + lsmessage, lsstatus);
                        }

                    });
                }
                else
                {

                    lsstatus = 'success';
                    lsmessage = 'Single Form Added Successfully';
                }
              
         

           
            

            });
            console.log(lsmessage, lsstatus);
            if (lsstatus == 'success') {
               
                Notify.alert(lsmessage, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });

            }
            else {
               
                Notify.alert(lsmessage, {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
           
            params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid,

            }
            activate();
           
            activate();


        }
      
    }



})();
