    (function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndAddQuestionController', FndAddQuestionController);

    FndAddQuestionController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndAddQuestionController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndAddQuestionController';

        $scope.Questionnarie_gid = $location.search().Questionnarie_gid;
        var Questionnarie_gid = $location.search().Questionnarie_gid;

        var invisible = false;
        activate();

        function activate() {

            $scope.rdbQmandatory = "No";

            var url = 'api/FndQuestionnarieMaster/GetCampaigntype';
            SocketService.get(url).then(function (resp) {
                $scope.campaigntypelist = resp.data.campaigntype_list;
            });


            var url = 'api/FndTrnCampaign/GetQuestionCategory';
            SocketService.get(url).then(function (resp) {
                $scope.categorytype_list = resp.data.category_list;

            });
        }
        var lsQuestionnarie_gid ;

        $scope.answerdesc_add = function () {

            if (($scope.txtAnswerDescription == undefined) || ($scope.txtAnswerDescription == '') ) {
                Notify.alert('Enter Answer description', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                return
            }
            if (($scope.cboCampaign_type == undefined) || ($scope.cboCampaign_type == '-----Select Campaign Type-----')) {
                Notify.alert('Enter Campaign Type', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtAnswerDescription = '';
                return
            }
            else {


                var params = {
                    campaigntype_gid: $scope.cboCampaign_type.campaigntype_gid,
                    answer_desc: $scope.txtAnswerDescription,
                    questionnarie_type: $scope.Q_type,
                    
                }
                var url = 'api/FndQuestionnarieMaster/PostAnswerDesc';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        lsQuestionnarie_gid = resp.data.Questionnarie_gid;
                        console.log('test' + lsQuestionnarie_gid);
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
                    mobileno_list(lsQuestionnarie_gid);
                    $scope.txtAnswerDescription = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                    $scope.Qtype_disabled = true;
                });
            }
        }

        //--------Delete Mobile No--------//
        $scope.answerdesc_delete = function (questionnarieanswer_gid) {
            var params =
                {
                    questionnarieanswer_gid: questionnarieanswer_gid
                }
            console.log(params)
            var url = 'api/FndQuestionnarieMaster/DeleteAnswerDesc';
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

                mobileno_list(lsQuestionnarie_gid);
                

            });

        }
        $scope.Campaign_typeChange = function () {
            $scope.cboCategory = '-----Select Category-----';
            $scope.txtQuestionnariename = "";
            $scope.Q_type = '-----Select Answer Type-----';
           
           $scope.invisible = true;
           $scope.Qtype_disabled = false;
           $scope.invisible = false;
        }
        $scope.TypeChange = function (data) {
            
            //$scope.invisible = $scope.invisible ? false : true;
            if (data == 'Text' || data == '' || data == 'Number' || data == '' || data == '-----Select Answer Type-----' ) {
                $scope.invisible = false;
            } else {
                $scope.invisible = true;
            }
        }
      

        function mobileno_list(lsQuestionnarie_gid) {
            var params = {
                Questionnarie_gid: lsQuestionnarie_gid,
            }
            var url = 'api/FndQuestionnarieMaster/GetAnswerDesc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.answerdesc_list = resp.data.questionnarieanswerlist;
                if(resp.data.length == null)
                {
                    $scope.Qtype_disabled = false;
                }
                else { $scope.Qtype_disabled = true; }
            });
        }
        var lsconstitution_gid = '';
        var lsconstitution_name = '';
        $scope.Back = function () {
            $state.go('app.FndMstQuestionBankMaster');
        }
       
        $scope.submit = function () {
            if (($scope.cboCampaign_type == undefined) || ($scope.cboCampaign_type == '-----Select Campaign Type-----')) {
                Notify.alert('Enter Campaign Type', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (($scope.cboCampaign_type == undefined) || ($scope.txtQuestionnariename == '')) {
                Notify.alert('Enter Questionnarie title', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (($scope.cboCategory == undefined) || ($scope.cboCategory == '-----Select Category-----')) {
                Notify.alert('Enter Select Category', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (($scope.Q_type == undefined) || ($scope.Q_type == '-----Select Answer Type-----')) {
                Notify.alert('Enter Answer Type', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            //else if ( ($scope.Q_type == 'List') || ($scope.Q_type == 'Radio_Button')) {
            //    //if (($scope.txtAnswerDescription == undefined) || ($scope.txtAnswerDescription == '')) {
            //    //    Notify.alert('Enter Answer Description', {
            //    //        status: 'warning',
            //    //        pos: 'top-center',
            //    //        timeout: 3000
            //    //    });
            //    //}
            //    //else if (($scope.answerdesc_list == undefined) || ($scope.answerdesc_list == '')) {
            //    //    Notify.alert('Add Answer Description', {
            //    //        status: 'warning',
            //    //        pos: 'top-center',
            //    //        timeout: 3000
            //    //    });
            //    //}
            //}
            else {
                var params = {
                    category_gid: $scope.cboCategory.categorytype_gid,
                    campaigntype_gid: $scope.cboCampaign_type.campaigntype_gid,
                    questionnarie_name: $scope.txtQuestionnariename,
                    questionnarie_type: $scope.Q_type,
                    rbo_mandatory: $scope.rdbQmandatory,
                    lms_code: $scope.txtLmscode,
                    remarks: $scope.txtaddremarks,
                    bureau_code: $scope.txtBureaucode,
                    Questionnarie_gid: lsQuestionnarie_gid,

                }

                var url = 'api/FndQuestionnarieMaster/CreateQuestionnarie';

                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.FndMstQuestionBankMaster');
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




    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndEditQuestionController', FndEditQuestionController);

    FndEditQuestionController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndEditQuestionController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndEditQuestionController';
        $scope.Questionnarie_gid = localStorage.getItem('Questionnarie_gid');
        var invisible = false;
        activate();

        function activate() {
            $scope.rdbQmandatory = "No";
            $scope.txtAnswerDescription == "";
           
            var param = {
                Questionnarie_gid: $scope.Questionnarie_gid
            };
            var url = 'api/FndQuestionnarieMaster/GetCampaigntype';
            
            SocketService.get(url).then(function (resp) {
                $scope.campaigntypelist = resp.data.campaigntype_list;
                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetQuestionCategory';
            SocketService.get(url).then(function (resp) {
                $scope.categorytype_list = resp.data.category_list;

            });
          
            var params = {
                Questionnarie_gid: $scope.Questionnarie_gid,
            }
            var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtEditQuestionnariename = resp.data.Questionnarie_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.rdbQmandatory = resp.data.mandatory;
                $scope.Q_type = resp.data.Questionnarie_type;
               // $scope.txtAnswerDescription = resp.data.Questionnarie_answer;
                $scope.txtaddremarks = resp.data.remarks;
                $scope.cboCategory = resp.data.categorytype_gid;


                var params = {
                    Questionnarie_gid: $scope.Questionnarie_gid,
                }
                var url = 'api/FndQuestionnarieMaster/GetEditAnswerDesc';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.answerdesc_list = resp.data.questionnarieanswerlist;

                });
               // mobileno_list($scope.Questionnarie_gid);
                
               

                
            
                if ($scope.Q_type == 'Text' || $scope.Q_type == '' || $scope.Q_type == 'Number' || $scope.Q_type == '') {
                    $scope.invisible = false;
                } else {
                    $scope.invisible = true;
                }
               
                unlockUI();
            });
           
        }
        $scope.Campaign_typeChange = function () {
            $scope.cboCategory = '-----Select Category-----';
            $scope.txtQuestionnariename = "";
            $scope.Q_type = '-----Select Answer Type-----';
            $scope.invisible = false;
        }
        $scope.answerdesc_add = function () {

            if (($scope.txtAnswerDescription == undefined) || ($scope.txtAnswerDescription == '')) {
                Notify.alert('Enter Answer description');
            }
            else {


                var params = {
                    answer_desc: $scope.txtAnswerDescription,
                    questionnarie_type: $scope.Q_type,
                    questionnarie_gid: $scope.Questionnarie_gid,

                }
                var url = 'api/FndQuestionnarieMaster/PostEditAnswerDesc';
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
                    var params = {
                        Questionnarie_gid: $scope.Questionnarie_gid,
                    }
                    var url = 'api/FndQuestionnarieMaster/GetEditAnswerDesc';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.answerdesc_list = resp.data.questionnarieanswerlist;
                        if (resp.data.length == null) {
                            $scope.Qtype_disabled = false;
                        }
                        else { $scope.Qtype_disabled = true; }
                    });
                    $scope.txtAnswerDescription = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                    $scope.Qtype_disabled = true;
                });
            }
        }
        $scope.answerdesc_delete = function (questionnarieanswer_gid) {
            var params =
                {
                    questionnarieanswer_gid: questionnarieanswer_gid
                }
            console.log(params)
            var url = 'api/FndQuestionnarieMaster/DeleteAnswerDesc';
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

                mobileno_list($scope.Questionnarie_gid);


            });

        }
        function mobileno_list(lsQuestionnarie_gid) {
            var params = {
                Questionnarie_gid: lsQuestionnarie_gid,
            }
            var url = 'api/FndQuestionnarieMaster/GetEditAnswerDesc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.answerdesc_list = resp.data.questionnarieanswerlist;
                if (resp.data.length == null) {
                    $scope.Qtype_disabled = false;
                }
                else { $scope.Qtype_disabled = true; }
            });
        }
        $scope.TypeChange = function (data) {

            //$scope.invisible = $scope.invisible ? false : true;
            if (data == 'Text' || data == '' || data == 'Number' || data == '') {
                $scope.invisible = false;
            } else {
                $scope.invisible = true;
            }
        }
        $scope.Back = function () {
            $state.go('app.FndMstQuestionBankMaster');
        }
        $scope.Edit_submit = function () {
            if (($scope.txtEditQuestionnariename == '') || ($scope.txtEditQuestionnariename == undefined) ) {
                Notify.alert('Please Fill Questionnarie title');
            }
            else {
              
                var params = {
                    campaigntype_gid: $scope.cboCampaign_type,
                    categorytype_gid: $scope.cboCategory,
                    questionnarie_name: $scope.txtEditQuestionnariename,
                    questionnarie_type: $scope.Q_type,
                    rbo_mandatory: $scope.rdbQmandatory,
                    lms_code: $scope.txtLmscode,
                    remarks: $scope.txtaddremarks,
                    bureau_code: $scope.txtBureaucode,
                    Questionnarie_gid: $scope.Questionnarie_gid,

                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEditSubmit';
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
                    $state.go('app.FndMstQuestionBankMaster');
                });
            }
        }

     
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCampaignTypeMasterController', FndMstCampaignTypeMasterController);

    FndMstCampaignTypeMasterController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndMstCampaignTypeMasterController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCampaignTypeMasterController';
        activate();


        function activate() {

            var url = 'api/FndMstCampaignTypeMaster/GetCampaignType';
            //var url = 'api/FndMstCampaignType/GetCampaignType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                //console.log(url);
                $scope.campaigntype_data = resp.data.campaigntype_list;
                unlockUI();
            });
        }

       

        $scope.popupcampaigntype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.campaigntypeSubmit = function () {
                    var params = {
                        campaigntype_gid: $scope.campaigntype_gid,
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_code: $scope.txtcampaigntype_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCampaignTypeMaster/CreateCampaignType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Campaign Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                            //Notify.alert('Error Occurred While Adding Campaign Type!', 'warning')

                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }

            }
        }

        $scope.editcampaigntype = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcampaigntype_code = resp.data.campaigntype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcampaign_type = resp.data.campaigntype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.campaigntype_gid = resp.data.campaigntype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.campaigntypeUpdate = function () {

                    var url = 'api/FndMstCampaignTypeMaster/UpdateCampaignType';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        campaigntype_code: $scope.txteditcampaigntype_code,
                        campaigntype_name: $scope.txteditcampaign_type,
                        remarks: $scope.txteditremarks,
                        campaigntype_gid: $scope.campaigntype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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






        $scope.showPopover = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcampaigntype_code = resp.data.campaigntype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcampaign_type = resp.data.campaigntype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.campaigntype_gid = resp.data.campaigntype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               

            }
        }


        

        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }

 
        $scope.deletecampaigntype = function (campaigntype_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstcustomerAddChequeController', FndMstcustomerAddChequeController);

    FndMstcustomerAddChequeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function FndMstcustomerAddChequeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstcustomerAddChequeController';
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).customer_gid;        
        var customer_gid = cmnfunctionService.decryptURL($location.search().hash).customer_gid;
        activate();

        function activate() {
             vm.open3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened3 = true;
            };
            vm.open4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened4 = true;
            };
            vm.open5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            

            //var url = 'api/FndMstCustomerMasterAdd/GetUdcTempClear';
            //SocketService.get(url).then(function (resp) {
            //});
                   
            $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
            $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;

                //var params = {
                //    application_gid: $scope.customer_gid
                //}
                //var url = 'api/UdcManagement/GetStakeholders';
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.StakeholderList = resp.data.StakeholderList;
                //});

                //$scope.onChangeStakeholderName = function (stakeholder_gid) {
                //    var list = $scope.StakeholderList;

                //    for (var i = 0; i < list.length; i++) {
                //        if (list[i].stakeholder_gid == stakeholder_gid) {
                //            $scope.txtstakeholder_type = list[i].stakeholder_type;
                //            $scope.txtdesignation = list[i].designation;
                //            break;
                //        }
                //    }

                //}

                //var url = 'api/UdcManagement/GetDropDownUdc';
                //lockUI();
                //SocketService.get(url).then(function (resp) {
                //    $scope.bankname_list = resp.data.bankname_list;
                //    unlockUI();
                //});




        }
        //$scope.add_cheque = function () {
        //    $location.url('app/MstUDCMakerAddCheque?lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit');

        //}

        $scope.back = function(){
            $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

        }

        //$scope.add_cheque = function () {
        //    $state.go('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
        //}

        //$scope.add_cheque = function () {
        //    var params ={
               
                
        //        accountholder_name:$scope.txtaccountholder_name,
        //        account_number :$scope.txtaccount_number,
        //        bank_name :$scope.txtbank_name,
        //        cheque_no :$scope.txtcheque_no,
        //        ifsc_code :$scope.txtifsc_code,
        //        micr :$scope.txtmicr,
        //        branch_address :$scope.txtbranch_address,
        //        branch_name :$scope.txtbranch_name,
        //        city :$scope.txtcity,
        //        district :$scope.txtdistrict, 
        //        state :$scope.txtstate
               
        //    }
        //    var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {

        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
                    
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $location.url('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
        //    });

            
        //}

        var url = 'api/FndMstCustomerMasterAdd/GetDropDownUdc';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.bankname_list = resp.data.bankname_list;
            unlockUI();
        });

        $scope.delete_cheque = function (customer_gid) {
            lockUI();
            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }
        
    
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
    }


    $scope.add_cheque = function () {

            var params = {              
                //accountholder_name: $scope.txtaccountholder_name,
                //account_number: $scope.txtaccount_number,
                //bank_name: $scope.txtbank_name,
                //cheque_no: $scope.txtcheque_no,
                //ifsc_code: $scope.txtifsc_code,
                //micr: $scope.txtmicr,
                //branch_address: $scope.txtbranch_address,
                //branch_name: $scope.txtbranch_name,
                //city: $scope.txtcity,
                //district: $scope.txtdistrict,
                //state: $scope.txtstate,


                //stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                //stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                //stakeholder_type: $scope.txtstakeholder_type,
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,            
                status_chequeclearance: $scope.txtstatus_chequeclearance
               
            }
            var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));
                    //$location.go('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lstab=edit');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();

              //  $scope.cboStakeholder = '',
              //$scope.txtstakeholder_type = '',
              //$scope.txtdesignation = '',
              $scope.txtaccountholder_name = '',
              $scope.txtaccount_number = '',
              $scope.txtbank_name = "",
              $scope.txtcheque_no = "",
              $scope.txtifsc_code = "",
              $scope.txtmicr = "",
              $scope.txtbranch_address = "",
              $scope.txtbranch_name = "",
              $scope.txtcity = "",
              $scope.txtdistrict = "",
              $scope.txtstate = "",
              $scope.cbomergedbanking_entity = "",
              $scope.txtspecial_condition = "",
              $scope.txtgeneral_remarks = "",
              $scope.rbocts_enabled = "",
              $scope.cbocheque_type = "",
              $scope.txtdate_chequetype = "",
              $scope.txtdate_chequepresentation = "",
              $scope.txtstatus_chequepresentation = "",
              $scope.txtdate_chequeclearance = "",
              $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;
                $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

            });
          

        }
    $scope.UploadDocument = function (val, val1, name) {
        var item = {
            name: val[0].name,
            file: val[0]
        };
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

        if (IsValidExtension == false) {
            Notify.alert("File format is not supported..!", {
                status: 'danger',
                pos: 'top-center',
                timeout: 3000
            });
            return false;
        }
        var frm = new FormData();
        frm.append('fileupload', item.file);
        frm.append('file_name', item.name);
        frm.append('document_name', $scope.documentname);
        frm.append('project_flag', "Default");
        $scope.uploadfrm = frm;
        if ($scope.uploadfrm != undefined) {
            var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
          
                if (resp.data.status == true){
                       var url = 'api/Kyc/ChequeOCR';
                       SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                           if (resp.data.statusCode == 101) {
                               $scope.txtaccountholder_name = resp.data.result.name[0];
                               $scope.txtaccount_number = resp.data.result.accNo;
                               $scope.txtbank_name = resp.data.result.bank;
                               $scope.txtcheque_no = resp.data.result.chequeNo;
                               $scope.txtifsc_code = resp.data.result.ifsc;
                               $scope.txtmicr = resp.data.result.micr;
                               $scope.txtbranch_address = resp.data.result.bankDetails.address;
                               $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                               $scope.txtcity = resp.data.result.bankDetails.city;
                               $scope.txtdistrict = resp.data.result.bankDetails.district;
                               $scope.txtstate = resp.data.result.bankDetails.state;
                           }
                           else {
                               Notify.alert('Error in fetching values from document..!', 'warning');
                           }
                       }); 

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
                $("#file").val('');
                $scope.uploadfrm = undefined;

                chequedocument_list();
                unlockUI();
            });
        }
        else {
            alert('Document is not Available..!');
            return;
        }
        
    }

    function chequedocument_list() {
        var url = 'api/FndMstCustomerMasterAdd/GetChequeDocumentList';
        SocketService.get(url).then(function (resp) {
            $scope.chequedocument_list = resp.data.chequedocument_list;
        });
    }


    $scope.delete_document = function (cheque2document_gid) {
        lockUI();
        var params = {
            cheque2document_gid: cheque2document_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentDelete';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.documentupload_list = resp.data.documentupload_list;
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            chequedocument_list();
            unlockUI();
        });
    }

        
}

    
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerApprovingMasterController', FndMstCustomerApprovingMasterController);

    FndMstCustomerApprovingMasterController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function FndMstCustomerApprovingMasterController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerApprovingMasterController';

        activate();

        function activate() {
            var url = 'api/FndMstCustomerApprovingMaster/GetCustomerApproving';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerapproving_list = resp.data.customerapproving_list;
                unlockUI();
            });


        }

        $scope.customerapprovingadd = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcustomerapproving.html',
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

                var url = 'api/FndMstCustomerMasterAdd/GetPendingCustomer';
                SocketService.get(url).then(function (resp) {
                    $scope.customerlist = resp.data.customerpending_list;

                });

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                
                $scope.submit = function () {

                    var lscustomer_gid = '';
                    var lscustomer_name = '';
                    if ($scope.cboCustomer != undefined || $scope.cboCustomer != null) {
                        lscustomer_gid = $scope.cboCustomer.customer_gid,
                        lscustomer_name = $scope.cboCustomer.customer_name;
                    }

                    var lsapprover_gid = '';
                    var lsapprover_name = '';
                    if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                        lsapprover_gid = $scope.cboemployee.employee_gid,
                        lsapprover_name = $scope.cboemployee.employee_name;
                    }


                    var params = {
                        approver_gid: lsapprover_gid,
                        approver_name: lsapprover_name,
                        customer_gid: lscustomer_gid,
                        customer_name: lscustomer_name,
                        customerapproving_code: $scope.txtcustomerapproving_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCustomerApprovingMaster/CreateCustomerApproving';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
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
        $scope.editcustomerapproving = function (customerapproving_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcustomerapproving.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });


                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {


                    $scope.txteditcustomerapproving_code = resp.data.customerapproving_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    //$scope.txtcustomer_name = resp.data.customer_gid;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.customerapproving_gid = resp.data.customerapproving_gid;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    //$scope.employee = resp.data.approver_name;
                    $scope.cboemployee_edit = resp.data.approver_gid;
                    //$scope.cboemployee_edit = [];
                    //if (resp.data.employee != null) {
                    //    var count = resp.data.employee.length;
                    //    for (var i = 0; i < count; i++) {
                    //        var indexs = $scope.cboemployee_editlist.findIndex(x => x.employee_gid === resp.data.employee[i].employee_gid);
                    //        $scope.cboemployee_edit.push($scope.cboemployee_editlist[indexs]);
                    //        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    //    }
                    //}

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

              

                //var url = 'api/FndMstCustomerMasterAdd/GetPendingCustomer';
                //SocketService.get(url).then(function (resp) {
                //    $scope.customerlist = resp.data.customerpending_list;

                //});

                $scope.update = function () {

                    var customer_name;
                    //var customername_index = $scope.customerlist.map(function (e) { return e.customer_gid }).indexOf($scope.txtcustomer_name);
                    //if (customername_index == -1) { customer_name = ''; } else { customer_name = $scope.customerlist[customername_index].customer_name; };

                    var approver_name;
                    //var approver_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployee_edit);
                    //if (approver_index == -1) { approver_name = ''; } else { approver_name = $scope.employee_list[approver_index].employee_name; };

                    var url = 'api/FndMstCustomerApprovingMaster/UpdateCustomerApproving';
                    var params = {
                        customerapproving_code: $scope.txteditcustomerapproving_code,
                        customer_name: $scope.txtcustomer_name,
                        //customer_name: customer_name,
                        approver_gid: $scope.cboemployee_edit,
                        ////approver_name: employee_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        remarks: $scope.txteditremarks,
                        customerapproving_gid: $scope.customerapproving_gid,
                        //employee: $scope.cboemployee_edit,


                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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
        $scope.showPopover = function (customerapproving_gid, customerapproving_name) {
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
                    customerapproving_gid: customerapproving_gid
                }
                lockUI();
                var url = 'api/FndMstCustomerApprovingMaster/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.customerapproving_name = resp.data.customerapproving_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.showsPopover = function (customerapproving_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {


                    $scope.txteditcustomerapproving_code = resp.data.customerapproving_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.customer_approving = resp.data.customerapproving_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.customerapproving_gid = resp.data.customerapproving_gid;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    $scope.employee = resp.data.employee;
                   

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

             
            }
        }
        $scope.Status_update = function (customerapproving_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscustomerapproving.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerapproving_gid = resp.data.customerapproving_gid
                    $scope.customer_approving = resp.data.customerapproving_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        customerapproving_gid: customerapproving_gid,
                        customerapproving_name: $scope.txtcustomerapproving_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCustomerApprovingMaster/InactiveCustomerApproving';
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

                    $modalInstance.close('closed');

                }

                var param = {
                    customerapproving_gid: customerapproving_gid
                }

                var url = 'api/FndMstCustomerApprovingMaster/CustomerApprovingInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.customerapprovinginactivelog_list = resp.data.customerapproving_list;
                    unlockUI();
                });

            }
        }

        $scope.deletecustomerapproving = function (customerapproving_gid) {
            var params = {
                customerapproving_gid: customerapproving_gid
            }


            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {

                    var url = 'api/FndMstCustomerApprovingMaster/DeleteCustomerApproving';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer Approving !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerChequeViewController', FndMstCustomerChequeViewController);

    FndMstCustomerChequeViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerChequeViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerChequeViewController';
        
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;
        $scope.tab = cmnfunctionService.decryptURL($location.search().hash).lstab;
        activate();
        function activate() {

            var params = {
                fndmanagement2cheque_gid: $scope.fndmanagement2cheque_gid
            }
           var url = 'api/FndMstCustomerMasterAdd/GetChequeView';
            SocketService.getparams(url, params).then(function (resp) {
                //$scope.cheque_list = resp.data.cheque_list;
                $scope.accountholder_name = resp.data.accountholder_name;
                $scope.account_number = resp.data.account_number;
                $scope.bank_name = resp.data.bank_name;
                $scope.cheque_no = resp.data.cheque_no;
                $scope.ifsc_code = resp.data.ifsc_code;
                $scope.micr = resp.data.micr;
                $scope.branch_address = resp.data.branch_address;
                $scope.branch_name = resp.data.branch_name;
                $scope.city = resp.data.city;
                $scope.district = resp.data.district;
                $scope.state = resp.data.state;
                $scope.cbomergedbankingentity_name = resp.data.mergedbankingentity_name;
                $scope.cheque_type = resp.data.cheque_type;
                $scope.date_chequetype = resp.data.date_chequetype;
                $scope.cts_enabled = resp.data.cts_enabled;
                $scope.date_chequepresentation = resp.data.date_chequepresentation;
                $scope.status_chequepresentation = resp.data.status_chequepresentation;
                $scope.date_chequeclearance = resp.data.date_chequeclearance;
                $scope.status_chequeclearance = resp.data.status_chequeclearance;
                $scope.special_condition = resp.data.special_condition;
                $scope.general_remarks = resp.data.general_remarks;
            });
        }

        $scope.back = function () {
            if ($scope.tab == "edit") {
                $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

                //$location.url('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit');
            }
            else if ($scope.tab == "add") {
                $location.url('app/FndMstCustomerMasterAdd');
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterAddController', FndMstCustomerMasterAddController);

    FndMstCustomerMasterAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function FndMstCustomerMasterAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterAddController';

        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
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
            vm.open4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened4 = true;
            };
            vm.open5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountLevel';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountType';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            //});

            var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getdesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getindividualproof';
            SocketService.get(url).then(function (resp) {
                $scope.individualproof_list = resp.data.individualproof_list;
            });



            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

        }
       

        $scope.onchangebusinessstartdate = function (val) {

            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.txtbusinessstart_date = '';
                    Notify.alert(resp.data.message, 'warning')
                }
            });

            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }



        var url = 'api/FndMstCustomerMasterAdd/GetDropDownUdc';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.bankname_list = resp.data.bankname_list;
            unlockUI();
        });
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
       $scope.getcustomerbasedGST = function () {
     
            var param = {
                pan: $scope.txtpan_no
            }
            var url = 'api/Kyc/PANNumber';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                    $scope.panvalidation = true;
                    $scope.txtcustomer_name = resp.data.result.name;
                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                    $scope.panvalidation = false;
                    Notify.alert('PAN is not verified..!', 'warning');
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });

     
        }

       $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                             
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    }  else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

        }

        $scope.Back = function () {
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.add_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
        }



        $scope.customer_save = function () {
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var lsamlcategory_gid = '';
            var lsamlcategory_name = '';
            var lsbusinesscategory_gid = '';
            var lsbusinesscategory_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsindividualproof_gid = '';
            var lsindividualproof_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
                lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
                lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
                lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            }
            if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
                lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
                lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            }
            if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
                lsdesignation_gid = $scope.cbodesignation.designation_gid;
                lsdesignation_type = $scope.cbodesignation.designation_type;
            }
            if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
                lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
                lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            }



            var params = {
                customer_name: $scope.txtcustomer_name,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: $scope.cboConstitution.constitution_gid,
                //constitution_name: $scope.cboConstitution.constitution_name,
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                amlcategory_gid: lsamlcategory_gid,
                amlcategory_name: lsamlcategory_name,
                //assessmentagency_gid: $scope.cboassessmentagency.assessmentagency_gid,
                //assessmentagency_name: $scope.cboassessmentagency.assessmentagency_name,
                //assessmentagencyrating_gid: $scope.cboassessmentagencyrating.assessmentagencyrating_gid,
                //assessmentagencyrating_name: $scope.cboassessmentagencyrating.assessmentagencyrating_name,
                rating_date: $scope.txtrating_date,
                //amlcategory_gid: $scope.cboamlcategory.amlcategory_gid,
                //amlcategory_name: $scope.cboamlcategory.amlcategory_name,
                //businesscategory_gid: $scope.cbobusinesscategory.businesscategory_gid,
                //businesscategory_name: $scope.cbobusinesscategory.businesscategory_name,
                businesscategory_gid: lsbusinesscategory_gid,
                businesscategory_name: lsbusinesscategory_name,
                //designation_gid: $scope.cbodesignation.designation_gid,
                //designation_type: $scope.cbodesignation.designation_type,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                //individualproof_gid: $scope.cboindividualproof.individualproof_gid,
                //individualproof_name: $scope.cboindividualproof.individualproof_name,
                individualproof_gid: lsindividualproof_gid,
                individualproof_name: lsindividualproof_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks,
                msme_radio:$scope.newDependency,
                msme_registration: $scope.txtdependency_name

            }
            var url = 'api/FndMstCustomerMasterAdd/customerSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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

        $scope.customer_submit = function () {

            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var lsamlcategory_gid = '';
            var lsamlcategory_name = '';
            var lsbusinesscategory_gid = '';
            var lsbusinesscategory_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsindividualproof_gid = '';
            var lsindividualproof_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
                lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
                lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
                lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            }
            if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
                lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
                lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            }
            if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
                lsdesignation_gid = $scope.cbodesignation.designation_gid;
                lsdesignation_type = $scope.cbodesignation.designation_type;
            }
            if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
                lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
                lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            }





            var params = {
                customer_name: $scope.txtcustomer_name,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                constitution_gid: $scope.cboConstitution.constitution_gid,
                constitution_name: $scope.cboConstitution.constitution_name,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                amlcategory_gid: lsamlcategory_gid,
                amlcategory_name: lsamlcategory_name,              
                //assessmentagency_gid: $scope.cboassessmentagency.assessmentagency_gid,
                //assessmentagency_name: $scope.cboassessmentagency.assessmentagency_name,
                //assessmentagencyrating_gid: $scope.cboassessmentagencyrating.assessmentagencyrating_gid,
                //assessmentagencyrating_name: $scope.cboassessmentagencyrating.assessmentagencyrating_name,
                rating_date: $scope.txtrating_date,
                //amlcategory_gid: $scope.cboamlcategory.amlcategory_gid,
                //amlcategory_name: $scope.cboamlcategory.amlcategory_name,
                businesscategory_gid: $scope.cbobusinesscategory.businesscategory_gid,
                businesscategory_name: $scope.cbobusinesscategory.businesscategory_name,
                designation_gid: $scope.cbodesignation.designation_gid,
                designation_type: $scope.cbodesignation.designation_type,
                individualproof_gid: $scope.cboindividualproof.individualproof_gid,
                individualproof_name: $scope.cboindividualproof.individualproof_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks,
                msme_registration: $scope.txtdependency_name,
                msme_radio: $scope.newDependency,

            }
            var url = 'api/FndMstCustomerMasterAdd/customerSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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

        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    gst_list();
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';

                });
            }
        }

        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
                status: 'info';
                pos: 'top-center';
                timeout: 3000;
            }
            else {


                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimarymobile_no,
                    whatsapp_mobileno: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                });
            }
        }

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (customer2mobileno_gid) {
            var params =
                {
                    customer2mobileno_gid: customer2mobileno_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteMobileNo';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                mobileno_list();
            });

        }



        function mobileno_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.customermobileno_list = resp.data.customermobileno_list;

            });
        }

        $scope.emailaddress_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimaryemail_address,
                }
                var url = 'api/FndMstCustomerMasterAdd/PostEmailAddress';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }

        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                emailaddress_list();
            });

        }

        function emailaddress_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

            });
        }

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                    
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_address: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state_name: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
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

        $scope.address_delete = function (customer2address_gid) {
            var params =
                {
                    customer2address_gid: customer2address_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteAddress';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                address_list();
            });

        }

        function address_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;

            });
        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.add_cheque = function () {
          
            if (($scope.txtaccount_number == undefined) || ($scope.txtaccount_number == '')|| ($scope.txtcheque_no == undefined) || ($scope.txtcheque_no == '')) {
                Notify.alert('Enter Cheque Details', 'warning');
            }
            else {

                var lsbankname_gid = '';
                var lsbankname_name = '';

                if ($scope.cbomergedbanking_entity != undefined || $scope.cbomergedbanking_entity != null) {
                    lsbankname_gid = $scope.cbomergedbanking_entity.bankname_gid;
                    lsbankname_name = $scope.cbomergedbanking_entity.bankname_name;
                }

            var params = {              
               
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                //mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                //mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                mergedbankingentity_gid: lsbankname_gid,
                mergedbankingentity_name: lsbankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance
               
            }
            
            
            var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
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
                cheque_list();

                //  $scope.cboStakeholder = '',
                //$scope.txtstakeholder_type = '',
                //$scope.txtdesignation = '',
                $scope.txtaccountholder_name = '',
                $scope.txtaccount_number = '',
                $scope.txtbank_name = "",
                $scope.txtcheque_no = "",
                $scope.txtifsc_code = "",
                $scope.txtmicr = "",
                $scope.txtbranch_address = "",
                $scope.txtbranch_name = "",
                $scope.txtcity = "",
                $scope.txtdistrict = "",
                $scope.txtstate = "",
                $scope.cbomergedbanking_entity = "",
                $scope.txtspecial_condition = "",
                $scope.txtgeneral_remarks = "",
                $scope.rbocts_enabled = "",
                $scope.cbocheque_type = "",
                $scope.txtdate_chequetype = "",
                $scope.txtdate_chequepresentation = "",
                $scope.txtstatus_chequepresentation = "",
                $scope.txtdate_chequeclearance = "",
                $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;
           

            });
            }


        }
        //var param = {
        //    fndmanagement2cheque_gid: $scope.fndmanagement2cheque_gid

        //}
        
        function cheque_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });

            }
        

        //$scope.onChangeBorrowerName = function (application_gid) {
        //    var params = {
        //        application_gid: application_gid
        //    }
        //    var url = 'api/FndMstCustomerMasterAdd/GetStakeholders';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.StakeholderList = resp.data.StakeholderList;
        //    });
        //    $scope.txtstakeholder_type = '';
        //    $scope.txtdesignation = '';
        //}

        //$scope.onChangeStakeholderName = function (stakeholder_gid) {
        //    var list = $scope.StakeholderList;

        //    for (var i = 0; i < list.length; i++) {
        //        if (list[i].stakeholder_gid == stakeholder_gid) {
        //            $scope.txtstakeholder_type = list[i].stakeholder_type;
        //            $scope.txtdesignation = list[i].designation;
        //            break;
        //        }
        //    }

        //}

        $scope.delete_cheque = function (fndmanagement2cheque_gid) {
            lockUI();
            var params = {
                fndmanagement2cheque_gid: fndmanagement2cheque_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }
        $scope.viewCheque = function (fndmanagement2cheque_gid) {
            $location.url('app/MstUDCMakerView?hash=' + cmnfunctionService.encryptURL('lsfndmanagement_gid=' + $scope.fndmanagement_gid + '&lstab=add'));
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
        }

        function chequedocument_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.chequedocument_list = resp.data.chequedocument_list;
            });
        }

          $scope.UploadDocument = function (val, val1, name) {
        var item = {
            name: val[0].name,
            file: val[0]
        };
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

        if (IsValidExtension == false) {
            Notify.alert("File format is not supported..!", {
                status: 'danger',
                pos: 'top-center',
                timeout: 3000
            });
            return false;
        }
        var frm = new FormData();
        frm.append('fileupload', item.file);
        frm.append('file_name', item.name);
        frm.append('document_name', $scope.documentname);
        frm.append('project_flag', "Default");
        $scope.uploadfrm = frm;
        if ($scope.uploadfrm != undefined) {
            var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
          
                if (resp.data.status == true){
                       var url = 'api/Kyc/ChequeOCR';
                       SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                           if (resp.data.statusCode == 101) {
                               $scope.txtaccountholder_name = resp.data.result.name[0];
                               $scope.txtaccount_number = resp.data.result.accNo;
                               $scope.txtbank_name = resp.data.result.bank;
                               $scope.txtcheque_no = resp.data.result.chequeNo;
                               $scope.txtifsc_code = resp.data.result.ifsc;
                               $scope.txtmicr = resp.data.result.micr;
                               $scope.txtbranch_address = resp.data.result.bankDetails.address;
                               $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                               $scope.txtcity = resp.data.result.bankDetails.city;
                               $scope.txtdistrict = resp.data.result.bankDetails.district;
                               $scope.txtstate = resp.data.result.bankDetails.state;
                           }
                           else {
                               Notify.alert('Error in fetching values from document..!', 'warning');
                           }
                       }); 

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
                $("#file").val('');
                $scope.uploadfrm = undefined;

                chequedocument_list();
                unlockUI();
            });
        }
        else {
            alert('Document is not Available..!');
            return;
        }
        
          }
          $scope.delete_document = function (cheque2document_gid) {
              lockUI();
              var params = {
                  cheque2document_gid: cheque2document_gid
              }
              var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentDelete';
              SocketService.getparams(url, params).then(function (resp) {
                  $scope.documentupload_list = resp.data.documentupload_list;
                  if (resp.data.status == true) {

                      Notify.alert(resp.data.message, {
                          status: 'success',
                          pos: 'top-center',
                          timeout: 3000
                      });
                  }
                  else {
                      Notify.alert(resp.data.message, {
                          status: 'danger',
                          pos: 'top-center',
                          timeout: 3000
                      });
                  }
                  chequedocument_list();
                  unlockUI();
              });
          }
        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}

        $scope.bank_delete = function (customer2bank_gid) {
            var params =
                {
                    customer2bank_gid: customer2bank_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteBank';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                bank_list();
            });

        }

        function bank_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetBankList';
            SocketService.get(url).then(function (resp) {
                $scope.customerbank_list = resp.data.customerbank_list;

            });
        }




    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterController', FndMstCustomerMasterController);

    FndMstCustomerMasterController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterController';

        activate();

        function activate() {
            var url = 'api/FndMstCustomerMasterAdd/GetcustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerViewCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpendingView_count = resp.data.customerpendingview_count;
                $scope.customerapprovedView_count = resp.data.customerapprovedview_count;
                $scope.customerrejectedView_count = resp.data.customerrejectedview_count;

            });
          
            //var url = 'api/FndTrnCampaign/GetCampaignCounts';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI()
            //    $scope.campaignpending_count = resp.data.campaignpending_count;
            //    $scope.rejected_count = resp.data.rejected_count;
            //    $scope.approved_count = resp.data.approved_count;
            //    $scope.closed_count = resp.data.closed_count;

            //});
        }
        $scope.approvecustomerview = function () {
            $state.go('app.FndTrnCustomerApprovedView');
        }
        $scope.rejectcustomerview = function () {
            $state.go('app.FndTrnCustomerRejectedView');
        }
        $scope.addcustomer = function () {
            $state.go('app.FndMstCustomerMasterAdd');
        }

        $scope.editcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterEdit');
            $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
        }

        $scope.viewcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndMstCustomerMasterView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid='+val));
        }
        $scope.Status_update = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscustomer.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {

                        customer_gid: customer_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    customer_gid: customer_gid
                }

                var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerstatus_list = resp.data.customerstatus_list;
                    unlockUI();
                });
            }
        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
               
            }
        }

        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterEditController', FndMstCustomerMasterEditController);

    FndMstCustomerMasterEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterEditController';
        
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;
        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

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

            var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
                $scope.employee_gid = resp.data.employee_gid;

            });

            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;
            });



            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customermobileno_list = resp.data.mobileno_list;
            });




            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.email_list;

            });



            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;
            });
            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.email_list = resp.data.email_list;
            //});

            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.customermobileno_list = resp.data.customermobileno_list;
            //});
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getdesignation';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getindividualproof';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualproof_list = resp.data.individualproof_list;
            });


            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });


            var params = {
                customer_gid: $scope.customer_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcustomer_code = resp.data.customer_code;
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
                $scope.cboconstitution = resp.data.constitution_gid;
                $scope.cboassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.rating_date = resp.data.rating_date;
                $scope.cboamlcategory = resp.data.amlcategory_gid;
                $scope.cin_no = resp.data.cin_no;
                $scope.txtpan_no = resp.data.pan_no;
                $scope.cbobusinesscategory = resp.data.businesscategory_gid;
                $scope.remarks = resp.data.remarks;
                $scope.status_remarks = resp.data.status_remarks;
                $scope.dependency_name = resp.data.msme_registration;
                $scope.txtcontactperson_fn = resp.data.contactperson_fn;
                $scope.txtcontactperson_mn = resp.data.contactperson_mn;
                $scope.txtcontactperson_ln = resp.data.contactperson_ln;
                $scope.cboindividualproof = resp.data.individualproof_gid;
                $scope.cbodesignation = resp.data.designation_gid;
                $scope.newDependency = resp.data.msme_radio;

                if ($scope.newDependency == 'Yes') {
                    $scope.new_dependency = true;
                    $scope.new_row = true;
                }
                else {
                    $scope.new_dependency = false;
                    $scope.new_row = false;
                }

                unlockUI();
            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerraisequery_list = resp.data.customerraisequery_list;
            });

            //var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            //SocketService.get(url).then(function (resp) {
            //    $scope.cheque_list = resp.data.cheque_list;
            //});

        }

        $scope.customer_save = function () {
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var lsamlcategory_gid = '';
            var lsamlcategory_name = '';
            var lsbusinesscategory_gid = '';
            var lsbusinesscategory_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsindividualproof_gid = '';
            var lsindividualproof_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
                lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
                lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
                lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            }
            if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
                lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
                lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            }
            if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
                lsdesignation_gid = $scope.cbodesignation.designation_gid;
                lsdesignation_type = $scope.cbodesignation.designation_type;
            }
            if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
                lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
                lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            }



            var params = {
                customer_name: $scope.txtcustomer_name,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: $scope.cboConstitution.constitution_gid,
                //constitution_name: $scope.cboConstitution.constitution_name,
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                amlcategory_gid: lsamlcategory_gid,
                amlcategory_name: lsamlcategory_name,
                //assessmentagency_gid: $scope.cboassessmentagency.assessmentagency_gid,
                //assessmentagency_name: $scope.cboassessmentagency.assessmentagency_name,
                //assessmentagencyrating_gid: $scope.cboassessmentagencyrating.assessmentagencyrating_gid,
                //assessmentagencyrating_name: $scope.cboassessmentagencyrating.assessmentagencyrating_name,
                rating_date: $scope.txtrating_date,
                //amlcategory_gid: $scope.cboamlcategory.amlcategory_gid,
                //amlcategory_name: $scope.cboamlcategory.amlcategory_name,
                //businesscategory_gid: $scope.cbobusinesscategory.businesscategory_gid,
                //businesscategory_name: $scope.cbobusinesscategory.businesscategory_name,
                businesscategory_gid: lsbusinesscategory_gid,
                businesscategory_name: lsbusinesscategory_name,
                //designation_gid: $scope.cbodesignation.designation_gid,
                //designation_type: $scope.cbodesignation.designation_type,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                //individualproof_gid: $scope.cboindividualproof.individualproof_gid,
                //individualproof_name: $scope.cboindividualproof.individualproof_name,
                individualproof_gid: lsindividualproof_gid,
                individualproof_name: lsindividualproof_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks,
                msme_radio: $scope.newDependency,
                msme_registration: $scope.txtdependency_name

            }
            var url = 'api/FndMstCustomerMasterAdd/customerSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.add_cheque = function () {
            //$location.url('app/FndMstcustomerAddCheque');

            $location.url('app/FndMstcustomerAddCheque?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit'));

        }

        var params = {
            customer_gid: $scope.customer_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        SocketService.getparams(url, params).then(function (resp) {

            $scope.accountholder_name = resp.data.accountholder_name;
            $scope.account_number = resp.data.account_number;
            $scope.bank_name = resp.data.bank_name;
            $scope.cheque_no = resp.data.cheque_no;
            $scope.ifsc_code = resp.data.ifsc_code;
            $scope.micr = resp.data.micr;
            $scope.branch_address = resp.data.branch_address;
            $scope.branch_name = resp.data.branch_name;
            $scope.city = resp.data.city;
            $scope.district = resp.data.district;
            $scope.state = resp.data.state;

        });

        $scope.onchangebusinessstartdate = function (val) {

            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.txtbusinessstart_date = '';
                    Notify.alert(resp.data.message, 'warning')
                }
               
            });

            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                //$scope.dependency_name = '';
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }
        $scope.getcustomerbasedGST = function () {

            var param = {
                pan: $scope.txtpan_no
            }
            var url = 'api/Kyc/PANNumber';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                    $scope.panvalidation = true;
                    $scope.txtcustomer_name = resp.data.result.name;
                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                    $scope.panvalidation = false;
                    Notify.alert('PAN is not verified..!', 'warning');
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });


        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();

                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

        }
        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
            }
            else {


                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimarymobile_no,
                    whatsapp_mobileno: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
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
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                });
            }
        }


        function mobileno_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.customermobileno_list = resp.data.customermobileno_list;

            });
        }



        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    gst_list();
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';

                });
            }
        }

        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }
        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_address: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state_name: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
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

        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }
        //$scope.address_edit = function (customer2address_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editaddressdetails.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.geocodingFailed = false;

        //        var url = 'api/AddressType/GetAddressTypeASC';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.addresstype_list = resp.data.addresstype_list;
        //        });

        //        var params = {
        //            customer2address_gid: customer2address_gid
        //        }
        //        var url = 'api/MstCreditStatusAdd/AddressDetailEdit';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.cboaddresstype = resp.data.address_typegid;
        //            $scope.rdbprimaryaddress = resp.data.primary_address;
        //            $scope.txtaddressline1 = resp.data.addressline1;
        //            $scope.txtaddressline2 = resp.data.addressline2;
        //            $scope.txtlandmark = resp.data.landmark;
        //            $scope.txtpostal_code = resp.data.postal_code;
        //            $scope.txtcity = resp.data.city;
        //            $scope.txttaluka = resp.data.taluka;
        //            $scope.txtdistrict = resp.data.district;
        //            $scope.txtstate = resp.data.state;
        //            $scope.txtcountry = resp.data.country;
        //            $scope.txtlatitude = resp.data.latitude;
        //            $scope.txtlongitude = resp.data.longitude;
        //            $scope.customer_gid = resp.data.customer_gid;
        //            $scope.customer2address_gid = resp.data.customer2address_gid;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.onchangepostal_code = function () {
        //            var params = {
        //                postal_code: $scope.txtpostal_code
        //            }
        //            var url = 'api/Mstcustomer/GetPostalCodeDetails';

        //            SocketService.getparams(url, params).then(function (resp) {
        //                $scope.txtcity = resp.data.city;
        //                $scope.txttaluka = resp.data.taluka;
        //                $scope.txtdistrict = resp.data.district;
        //                $scope.txtstate = resp.data.state_name;
        //            });
        //        }

        //        $scope.getGeoCoding = function () {
        //            if ($scope.txtpostal_code.length == 6) {
        //                if ($scope.txtaddressline2 == undefined) {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
        //                } else {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
        //                }
        //                var params = {
        //                    address: addressString
        //                }
        //                var url = 'api/GoogleMapsAPI/GetGeoCoding';
        //                SocketService.getparams(url, params).then(function (resp) {
        //                    if (resp.data.status == "OK") {
        //                        $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
        //                        $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
        //                        $scope.geocodingFailed = false;
        //                    }
        //                    else if (resp.data.status == "ZERO_RESULTS") {
        //                        $scope.geocodingFailed = true;
        //                    }
        //                });
        //            }
        //        }

        //        $scope.txtcountry = "India";
        //        $scope.addressUpdate = function () {
        //            var address_type = $('#address_type :selected').text();

        //            var params = {
        //                address_typegid: $scope.cboaddresstype,
        //                address_type: address_type,
        //                addressline1: $scope.txtaddressline1,
        //                addressline2: $scope.txtaddressline2,
        //                primary_address: $scope.rdbprimaryaddress,
        //                landmark: $scope.txtlandmark,
        //                postal_code: $scope.txtpostal_code,
        //                taluka: $scope.txttaluka,
        //                city: $scope.txtcity,
        //                state: $scope.txtstate,
        //                district: $scope.txtdistrict,
        //                country: $scope.txtcountry,
        //                latitude: $scope.txtlatitude,
        //                longitude: $scope.txtlongitude,
        //                customer2address_gid: $scope.customer2address_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/MstCreditStatusAdd/AddressDetailUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                address_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.gst_edit = function (customer2gst_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgstdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });

                var params = {
                    customer2gst_gid: customer2gst_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/GSTEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditgst_state = resp.data.gststate_name;
                    $scope.txteditgst_number = resp.data.gst_no;
                    $scope.rdbgstregistered = resp.data.gstregister_status;
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2gst_gid = resp.data.customer2gst_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangeeditgst_number = function () {
                    var gst_number = $scope.txteditgst_number;
                    var params = {
                        gst_code: gst_number.substring(0, 2)
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditgst_state = resp.data.gst_state;
                    });
                }

                $scope.update_gst = function () {

                    var params = {
                        gststate_name: $scope.txteditgst_state,
                        gst_no: $scope.txteditgst_number,
                        gstregister_status: $scope.rdbgstregistered,
                        customer_gid: localStorage.getItem('customer_gid'),
                        customer2gst_gid: $scope.customer2gst_gid,
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GSTUpdate';
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
                        gst_templist();
                        $scope.txtgst_no = '';

                    });

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.emailaddress_edit = function (customer2emailaddress_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editemailaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_emailaddress: $scope.rdbeditprimary_emailaddress,
                        customer2emailaddress_gid: customer2emailaddress_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
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
                        emailaddress_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.mobileno_edit = function (customer2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2mobileno_gid: customer2mobileno_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/MobileNoEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimarymobile_no = resp.data.primary_mobileno;
                    $scope.rdbeditwhatsappmobile_no = resp.data.whatsapp_mobileno;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_mobileno: $scope.rdbeditprimarymobile_no,
                        whatsapp_mobileno: $scope.rdbeditwhatsappmobile_no,
                        customer2mobileno_gid: customer2mobileno_gid,
                        customer_gid: localStorage.getItem('customer_gid'),

                    }
                    var url = 'api/FndMstCustomerMasterAdd/MobileNoUpdate';
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
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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

                emailaddress_templist();
            });

        }

        $scope.Back = function () {
            //customerupdate();
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.edit_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
        }
        $scope.viewcustomer = function (customer_gid, fndmanagement2cheque_gid) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndMstCustomerChequeView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + customer_gid + '&lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lstab=edit'));
        }
        $scope.editCheque = function (udcmanagement2cheque_gid) {
            $location.url('app/FndMstcustomerEditCheque?hash=' + cmnfunctionService.encryptURL('lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lsfndmanagement_gid=' + $scope.fndmanagement_gid + '&lstab=edit'));
        }



        $scope.customer_submit = function () {
            //var lsconstitution_gid = '';
            //var lsconstitution_name = '';
            //var lsassessmentagency_gid = '';
            //var lsassessmentagency_name = '';
            //var lsassessmentagencyrating_gid = '';
            //var lsassessmentagencyrating_name = '';
            //var lsamlcategory_gid = '';
            //var lsamlcategory_name = '';
            //var lsbusinesscategory_gid = '';
            //var lsbusinesscategory_name = '';
            //var lsdesignation_gid = '';
            //var lsdesignation_type = '';
            //var lsindividualproof_gid = '';
            //var lsindividualproof_name = '';

            //if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
            //    lsconstitution_gid = $scope.cboConstitution.constitution_gid;
            //    lsconstitution_name = $scope.cboConstitution.constitution_name;
            //}
            //if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}
            //if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
            //    lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
            //    lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            //}
            //if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
            //    lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
            //    lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            //}

            //if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
            //    lsdesignation_gid = $scope.cbodesignation.designation_gid;
            //    lsdesignation_type = $scope.cbodesignation.designation_type;
            //}
            //if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
            //    lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
            //    lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            //}

            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboConstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customer_editupdate = function () {
            //var lsconstitution_gid = '';
            //var lsconstitution_name = '';
            //var lsassessmentagency_gid = '';
            //var lsassessmentagency_name = '';
            //var lsassessmentagencyrating_gid = '';
            //var lsassessmentagencyrating_name = '';
            //var lsamlcategory_gid = '';
            //var lsamlcategory_name = '';
            //var lsbusinesscategory_gid = '';
            //var lsbusinesscategory_name = '';
            //var lsdesignation_gid = '';
            //var lsdesignation_type = '';
            //var lsindividualproof_gid = '';
            //var lsindividualproof_name = '';

            //if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
            //    lsconstitution_gid = $scope.cboConstitution.constitution_gid;
            //    lsconstitution_name = $scope.cboConstitution.constitution_name;
            //}
            //if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}
            //if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
            //    lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
            //    lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            //}
            //if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
            //    lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
            //    lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            //}

            //if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
            //    lsdesignation_gid = $scope.cbodesignation.designation_gid;
            //    lsdesignation_type = $scope.cbodesignation.designation_type;
            //}
            //if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
            //    lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
            //    lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            //}

            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboconstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditupdated';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        function customerupdate()
        {
        
            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboconstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditupdated';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customer_update = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customersubmit = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/customersubmitapproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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


        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        //$scope.mobileno_edit = function () {

        //    if (($scope.mobile_no == undefined) || ($scope.mobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
        //        Notify.alert('Enter Mobile No/Select Status');
        //    }
        //    else {


        //        var params = {
        //            mobile_no: $scope.txtmobile_no,
        //            primary_mobileno: $scope.rdbprimarymobile_no,
        //            whatsapp_mobileno: $scope.rdbwhatsappmobile_no
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            customermobileno_list();
        //            $scope.txtmobile_no = '';
        //            $scope.rdbprimarymobile_no = '';
        //            $scope.rdbwhatsappmobile_no = '';
        //            $scope.rdbprimarymobile_no == false;
        //        });
        //    }
        //}

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (customer2mobileno_gid) {
            var params =
                {
                    customer2mobileno_gid: customer2mobileno_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteMobileNo';
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

                mobileno_list();
            });

        }


        $scope.cheque_delete = function (fndmanagement2cheque_gid) {
            var params =
                {
                    fndmanagement2cheque_gid: fndmanagement2cheque_gid
                }
            //console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
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

                customermobileno_list();
            });

        }

        //$scope.emailaddress_edit = function (customer2emailaddress_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editemailaddress.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer2emailaddress_gid: customer2emailaddress_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.txteditemail_address = resp.data.email_address;
        //            $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update_emailaddress = function () {

        //            var params = {
        //                email_address: $scope.txteditemail_address,
        //                primary_emailaddress: $scope.rdbeditprimary_emailaddress,
        //                customer2emailaddress_gid: customer2emailaddress_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                emailaddress_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

        function emailaddress_templist() {
            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });
        }
        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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

                emailaddress_list();
            });

        }

        function emailaddress_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

            });
        }
        $scope.address_edit = function (customer2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editaddressdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    customer2address_gid: customer2address_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/AddressDetailEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboaddresstype = resp.data.address_typegid;
                    $scope.rdbprimaryaddress = resp.data.primary_address;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2address_gid = resp.data.customer2address_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                    
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressUpdate = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboaddresstype,
                        address_type: address_type,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        primary_address: $scope.rdbprimaryaddress,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        customer2address_gid: $scope.customer2address_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/AddressDetailUpdate';
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
                        address_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }


        $scope.address_delete = function (customer2address_gid) {
            var params =
                {
                    customer2address_gid: customer2address_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteAddress';
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

                address_list();
            });

        }
        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    gst_list();
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';

                });
            }
        }
        $scope.emailaddress_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimaryemail_address,
                }
                var url = 'api/FndMstCustomerMasterAdd/PostEmailAddress';
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
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        function address_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;

            });
        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.edit_cheque = function () {
            var params = {
                stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                stakeholder_type: $scope.txtstakeholder_type,
                designation: $scope.txtdesignation,
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance
            }
            var url = 'api/UdcManagement/PostChequeDetail';
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
                cheque_list();

                $scope.cboStakeholder = '',
              $scope.txtstakeholder_type = '',
              $scope.txtdesignation = '',
              $scope.txtaccountholder_name = '',
              $scope.txtaccount_number = '',
              $scope.txtbank_name = "",
              $scope.txtcheque_no = "",
              $scope.txtifsc_code = "",
              $scope.txtmicr = "",
              $scope.txtbranch_address = "",
              $scope.txtbranch_name = "",
              $scope.txtcity = "",
              $scope.txtdistrict = "",
              $scope.txtstate = "",
              $scope.cbomergedbanking_entity = "",
              $scope.txtspecial_condition = "",
              $scope.txtgeneral_remarks = "",
              $scope.rbocts_enabled = "",
              $scope.cbocheque_type = "",
              $scope.txtdate_chequetype = "",
              $scope.txtdate_chequepresentation = "",
              $scope.txtstatus_chequepresentation = "",
              $scope.txtdate_chequeclearance = "",
              $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;


            });


        }
        function cheque_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
        }

        $scope.delete_cheque = function (udcmanagement2cheque_gid) {
            lockUI();
            var params = {
                udcmanagement2cheque_gid: udcmanagement2cheque_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }

        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}

        $scope.bank_delete = function (customer2bank_gid) {
            var params =
                {
                    customer2bank_gid: customer2bank_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteBank';
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

                bank_list();
            });

        }

        function bank_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetBankList';
            SocketService.get(url).then(function (resp) {
                $scope.customerbank_list = resp.data.customerbank_list;

            });
        }


        $scope.myraisequery = function (customer_gid) {
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
                    customer_gid: customer_gid
                }

                $scope.submit = function () {


                    var params = {
                        customer_gid: customer_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,

                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostCustomerRaiseQuery';
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
                            query_list(customer_gid);
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


        function query_list(customer_gid) {
            var params = {
                customer_gid: customer_gid,

            }

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerraisequery_list = resp.data.customerraisequery_list;
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

        $scope.query_close = function (customerraisequery_gid) {
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
                        customer_gid : cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid,
                        customerraisequery_gid: customerraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                       
                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostCustomerresponsequery';
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


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterViewController', FndMstCustomerMasterViewController);

    FndMstCustomerMasterViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterViewController';
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        
      
        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

           

            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountLevel';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountType';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            //SocketService.get(url).then(function (resp) {
            //    $scope.constitution_list = resp.data.constitution_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagency_list = resp.data.assessmentagency_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.amlcategory_list = resp.data.amlcategory_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.businesscategory_list = resp.data.businesscategory_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/state';
            //SocketService.get(url).then(function (resp) {
            //    $scope.state_list = resp.data.state_list;
            //});



         
                var param = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/FndMstCustomerMasterAdd/customerGSTList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.customergst_list = resp.data.customergst_list;
                });
    

         
                var param = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/FndMstCustomerMasterAdd/customerMobileNoList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mobileno_list = resp.data.mobileno_list;
                });
  



                var param = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.email_list = resp.data.email_list;

                });
            

         
                var param = {
                    customer_gid: $scope.customer_gid

                };
                var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.address_list = resp.data.customeraddress_list;
                });
            


            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_code = resp.data.customer_code;
                $scope.customer_name = resp.data.customer_name;
                $scope.businessstart_date = resp.data.businessstart_date;
                $scope.year_business = resp.data.year_business;
                $scope.month_business = resp.data.month_business;
                $scope.constitution_name = resp.data.constitution_name;
                $scope.assessmentagency_name = resp.data.assessmentagency_name;
                $scope.assessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.rating_date = resp.data.rating_date;
                $scope.amlcategory_name = resp.data.amlcategory_name;
                $scope.cin_no = resp.data.cin_no;
                $scope.pan_no = resp.data.pan_no;
                $scope.businesscategory_name = resp.data.businesscategory_name;
                $scope.remarks = resp.data.remarks;
                $scope.msme_registration = resp.data.msme_registration;
                $scope.contactperson_fn = resp.data.contactperson_fn;
                $scope.contactperson_mn = resp.data.contactperson_mn;
                $scope.contactperson_ln = resp.data.contactperson_ln;
                $scope.individualproof_name = resp.data.individualproof_name;
                $scope.designation_type = resp.data.designation_type;

                //if (resp.data.credit_status == 'Pending') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //} else if (resp.data.credit_status == 'Completed') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //}
                //else {
                //    $scope.showsubmit = true;
                //    $scope.showupdate = false;
                //}

                unlockUI();
            });

        }

        var params = {
            customer_gid: $scope.customer_gid
        }
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });

        
        //var params={
        //    customer_gid: $scope.customer_gid
        //}
        //var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        //SocketService.getparams(url, params).then(function (resp) {
          
        //    $scope.accountholder_name = resp.data.accountholder_name;
        //    $scope.account_number = resp.data.account_number;
        //    $scope.bank_name = resp.data.bank_name;
        //    $scope.cheque_no = resp.data.cheque_no;
        //    $scope.ifsc_code = resp.data.ifsc_code;
        //    $scope.micr = resp.data.micr;
        //    $scope.branch_address = resp.data.branch_address;
        //    $scope.branch_name = resp.data.branch_name;
        //    $scope.city = resp.data.city;
        //    $scope.district = resp.data.district;
        //    $scope.state = resp.data.state;
        //    $scope.mergedbankingentity_name = resp.data.mergedbankingentity_name;
        //    $scope.special_condition = resp.data.special_condition;
        //    $scope.general_remarks = resp.data.general_remarks;
        //    $scope.cts_enabled = resp.data.cts_enabled;
        //    $scope.cheque_type = resp.data.cheque_type;
        //    $scope.date_chequetype = resp.data.date_chequetype;
        //    $scope.date_chequepresentation = resp.data.date_chequepresentation;
        //    $scope.status_chequepresentation = resp.data.status_chequepresentation;
        //    $scope.date_chequeclearance = resp.data.date_chequeclearance;
        //    $scope.status_chequeclearance = resp.data.status_chequeclearance;
           
        //});
    
            //var url = 'api/FndMstCustomerMasterAdd/GetChequeDetails';
            //SocketService.get(url).then(function (resp) {
            //    $scope.cheque_list = resp.data.cheque_list;
            //});
       
        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.Back = function () {
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.edit_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
        }



   
        //function gst_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customergst_list = resp.data.customergst_list;

        //    });
        //}




     

     

        //function emailaddress_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

        //    });
        //}



      

        //function address_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeraddress_list = resp.data.customeraddress_list;

        //    });
        //}

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

       
        function cheque_list() {
            var url = 'api/UdcManagement/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
        }

       

        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}





    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstQuestionBankMaster', FndMstQuestionBankMaster);

        FndMstQuestionBankMaster.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function FndMstQuestionBankMaster($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstQuestionBankMaster';
       // console.log('test');
        activate();

        function activate() {
            var url = 'api/FndQuestionnarieMaster/GetQuestionnarie';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.questionarie = resp.data.Questionnarie_list;
                unlockUI();
            });
        }
        $scope.addQuestionTitle = function () {
            $state.go('app.FndAddQuestion');
        }
        $scope.Back = function () {
            activate();
            $state.go('app.FndMstQuestionBankMaster');
        }

        $scope.edit = function (val) {
            localStorage.setItem('Questionnarie_gid', val);
            $state.go('app.FndEditQuestion');
        }
        $scope.delete = function (Questionnarie_gid) {
            var params = {
                Questionnarie_gid: Questionnarie_gid
            }


            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {

                    var url = 'api/FndQuestionnarieMaster/DeleteQuestionnarie';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Questionnarie !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.showPopover = function (Questionnarie_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    Questionnarie_gid: Questionnarie_gid
                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



            }
        }

        //Divya

        $scope.importexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
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

                $scope.downloadtemplate_importexcel = function () {
                  
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportQuestionsMaster.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                  
                    /* var filename = "ImportQuestionsMaster.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = "E:\\Web\\EMS\\templates\\ImportQuestionsMaster.xlsx";
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = "http://"
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();*/
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
                    else if (filePath.includes("ImportQuestionsMaster") == false) {
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
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        frm.append('project_flag', "RSK");
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        var url = 'api/FndQuestionnarieMaster/ImportExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');

                            if (resp.data.status == true) {
                                //  defaultdynamic();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            activate();
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

        //Divya


        $scope.Status_update = function (Questionnarie_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusQuestionnarie.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    Questionnarie_gid: Questionnarie_gid
                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        Questionnarie_name: $scope.Questionnarie_name,
                        Questionnarie_gid: Questionnarie_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndQuestionnarieMaster/InactiveQuestionnarie';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    Questionnarie_gid: Questionnarie_gid
                }

                var url = 'api/FndQuestionnarieMaster/QuestionnarieInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.statusQuestionnarieinactivelog_list = resp.data.Questionnarie_list;
                    unlockUI();
                });
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstQuestionnarieCategoryController', FndMstQuestionnarieCategoryController);

    FndMstQuestionnarieCategoryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndMstQuestionnarieCategoryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstQuestionnarieCategoryController';
        activate();


        function activate() {

            var url = 'api/FndMstCategoryTypeMaster/GetCategoryType';

            lockUI();
            SocketService.get(url).then(function (resp) {
                console.log(url);
                $scope.categorytype_data = resp.data.categorytype_list;
                unlockUI();
            });
        }

        $scope.popupcategorytype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.categorytypeSubmit = function () {
                    var params = {
                        categorytype_gid: $scope.categorytype_gid,
                        categorytype_name: $scope.txtcategory_type,
                        categorytype_code: $scope.txtcategorytype_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCategoryTypeMaster/CreateCategoryType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')


                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }

            }
        }

        $scope.editcategorytype = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcategorytype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcategorytype_code = resp.data.categorytype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcategory_type = resp.data.categorytype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.categorytype_gid = resp.data.categorytype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.categorytypeUpdate = function () {

                    var url = 'api/FndMstCategoryTypeMaster/UpdateCategoryType';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        categorytype_code: $scope.txteditcategorytype_code,
                        categorytype_name: $scope.txteditcategory_type,
                        remarks: $scope.txteditremarks,
                        categorytype_gid: $scope.categorytype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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


        $scope.showPopover = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcategorytype_code = resp.data.categorytype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcategory_type = resp.data.categorytype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.categorytype_gid = resp.data.categorytype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               

            }
        }

        $scope.Status_update = function (categorytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscategorytype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    categorytype_gid: categorytype_gid
                }
                var url = 'api/FndMstCategoryTypeMaster/EditCategoryType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.categorytype_gid = resp.data.categorytype_gid
                    $scope.txtcategory_type = resp.data.categorytype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        categorytype_name: $scope.txtcategory_type,
                        categorytype_gid: $scope.categorytype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCategoryTypeMaster/InactiveCategoryType';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    categorytype_gid: categorytype_gid
                }

                var url = 'api/FndMstCategoryTypeMaster/CategoryTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.categorytypeinactivelog_data = resp.data.categorytype_list;
                    unlockUI();
                });
            }
        }


        $scope.deletecategorytype = function (categorytype_gid) {
            var params = {
                categorytype_gid: categorytype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCategoryTypeMaster/DeleteCategoryType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Category Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnApprovalEditController', FndTrnApprovalEditController);

    FndTrnApprovalEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnApprovalEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnApprovalEditController';        
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;
        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
                $scope.employee_gid = resp.data.employee_gid;

            });

            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;
            });



            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });




            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });

           

           

            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;
            });
            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.email_list = resp.data.email_list;
            //});

            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.customermobileno_list = resp.data.customermobileno_list;
            //});
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getdesignation';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getindividualproof';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualproof_list = resp.data.individualproof_list;
            });


            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });


            var params = {
                customer_gid: $scope.customer_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_code = resp.data.customer_code;
                $scope.customer_name = resp.data.customer_name;
                $scope.businessstart_date = resp.data.businessstart_date;
                $scope.year_business = resp.data.year_business;
                $scope.month_business = resp.data.month_business;
                $scope.cboconstitution = resp.data.constitution_gid;
                $scope.cboassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.rating_date = resp.data.rating_date;
                $scope.cboamlcategory = resp.data.amlcategory_gid;
                $scope.cin_no = resp.data.cin_no;
                $scope.pan_no = resp.data.pan_no;
                $scope.cbobusinesscategory = resp.data.businesscategory_gid;
                $scope.remarks = resp.data.remarks;
                $scope.dependency_name = resp.data.msme_registration;
                $scope.contactperson_fn = resp.data.contactperson_fn;
                $scope.contactperson_mn = resp.data.contactperson_mn;
                $scope.contactperson_ln = resp.data.contactperson_ln;
                $scope.cboindividualproof = resp.data.individualproof_gid;
                $scope.cbodesignation = resp.data.designation_gid;
                $scope.newDependency = resp.data.msme_radio;
                $scope.txtAnswerDescription = '';

                if ($scope.newDependency == 'Yes') {
                    $scope.new_dependency = true;
                    $scope.new_row = true;
                }
                else {
                    $scope.new_dependency = false;
                    $scope.new_row = false;
                }

                unlockUI();
            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            //lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.customerraisequery_list = resp.data.customerraisequery_list;
                //unlockUI();
            });

            //var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            //SocketService.get(url).then(function (resp) {
            //    $scope.cheque_list = resp.data.cheque_list;
            //});

        }
        var params = {
            customer_gid: $scope.customer_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        SocketService.getparams(url, params).then(function (resp) {

            $scope.accountholder_name = resp.data.accountholder_name;
            $scope.account_number = resp.data.account_number;
            $scope.bank_name = resp.data.bank_name;
            $scope.cheque_no = resp.data.cheque_no;
            $scope.ifsc_code = resp.data.ifsc_code;
            $scope.micr = resp.data.micr;
            $scope.branch_address = resp.data.branch_address;
            $scope.branch_name = resp.data.branch_name;
            $scope.city = resp.data.city;
            $scope.district = resp.data.district;
            $scope.state = resp.data.state;

        });

        $scope.onchangebusinessstartdate = function (val) {

            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.txtbusinessstart_date = '';
                    Notify.alert(resp.data.message, 'warning')
                }
            });

            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }
        $scope.getcustomerbasedGST = function () {

            var param = {
                pan: $scope.txtpan_no
            }
            var url = 'api/Kyc/PANNumber';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                    $scope.panvalidation = true;
                    $scope.txtcustomer_name = resp.data.result.name;
                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                    $scope.panvalidation = false;
                    Notify.alert('PAN is not verified..!', 'warning');
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });


        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();

                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

        }
        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
            }
            else {


                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimarymobile_no,
                    whatsapp_mobileno: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
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
                    mobileno_templist();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                });
            }
        }
        function mobileno_templist() {
            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/MobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.customermobileno_list;
            });
        }
        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    gst_list();
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';

                });
            }
        }

        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }
        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                    
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_address: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state_name: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
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

        $scope.add_cheque = function () {
            //$location.url('app/FndMstcustomerAddCheque');

            $location.url('app/FndMstcustomerAddCheque?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit'));

        }

        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }
        //$scope.address_edit = function (customer2address_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editaddressdetails.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.geocodingFailed = false;

        //        var url = 'api/AddressType/GetAddressTypeASC';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.addresstype_list = resp.data.addresstype_list;
        //        });

        //        var params = {
        //            customer2address_gid: customer2address_gid
        //        }
        //        var url = 'api/MstCreditStatusAdd/AddressDetailEdit';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.cboaddresstype = resp.data.address_typegid;
        //            $scope.rdbprimaryaddress = resp.data.primary_address;
        //            $scope.txtaddressline1 = resp.data.addressline1;
        //            $scope.txtaddressline2 = resp.data.addressline2;
        //            $scope.txtlandmark = resp.data.landmark;
        //            $scope.txtpostal_code = resp.data.postal_code;
        //            $scope.txtcity = resp.data.city;
        //            $scope.txttaluka = resp.data.taluka;
        //            $scope.txtdistrict = resp.data.district;
        //            $scope.txtstate = resp.data.state;
        //            $scope.txtcountry = resp.data.country;
        //            $scope.txtlatitude = resp.data.latitude;
        //            $scope.txtlongitude = resp.data.longitude;
        //            $scope.customer_gid = resp.data.customer_gid;
        //            $scope.customer2address_gid = resp.data.customer2address_gid;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.onchangepostal_code = function () {
        //            var params = {
        //                postal_code: $scope.txtpostal_code
        //            }
        //            var url = 'api/Mstcustomer/GetPostalCodeDetails';

        //            SocketService.getparams(url, params).then(function (resp) {
        //                $scope.txtcity = resp.data.city;
        //                $scope.txttaluka = resp.data.taluka;
        //                $scope.txtdistrict = resp.data.district;
        //                $scope.txtstate = resp.data.state_name;
        //            });
        //        }

        //        $scope.getGeoCoding = function () {
        //            if ($scope.txtpostal_code.length == 6) {
        //                if ($scope.txtaddressline2 == undefined) {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
        //                } else {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
        //                }
        //                var params = {
        //                    address: addressString
        //                }
        //                var url = 'api/GoogleMapsAPI/GetGeoCoding';
        //                SocketService.getparams(url, params).then(function (resp) {
        //                    if (resp.data.status == "OK") {
        //                        $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
        //                        $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
        //                        $scope.geocodingFailed = false;
        //                    }
        //                    else if (resp.data.status == "ZERO_RESULTS") {
        //                        $scope.geocodingFailed = true;
        //                    }
        //                });
        //            }
        //        }

        //        $scope.txtcountry = "India";
        //        $scope.addressUpdate = function () {
        //            var address_type = $('#address_type :selected').text();

        //            var params = {
        //                address_typegid: $scope.cboaddresstype,
        //                address_type: address_type,
        //                addressline1: $scope.txtaddressline1,
        //                addressline2: $scope.txtaddressline2,
        //                primary_address: $scope.rdbprimaryaddress,
        //                landmark: $scope.txtlandmark,
        //                postal_code: $scope.txtpostal_code,
        //                taluka: $scope.txttaluka,
        //                city: $scope.txtcity,
        //                state: $scope.txtstate,
        //                district: $scope.txtdistrict,
        //                country: $scope.txtcountry,
        //                latitude: $scope.txtlatitude,
        //                longitude: $scope.txtlongitude,
        //                customer2address_gid: $scope.customer2address_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/MstCreditStatusAdd/AddressDetailUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                address_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.gst_edit = function (customer2gst_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgstdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });

                var params = {
                    customer2gst_gid: customer2gst_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/GSTEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditgst_state = resp.data.gststate_name;
                    $scope.txteditgst_number = resp.data.gst_no;
                    $scope.rdbgstregistered = resp.data.gstregister_status;
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2gst_gid = resp.data.customer2gst_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangeeditgst_number = function () {
                    var gst_number = $scope.txteditgst_number;
                    var params = {
                        gst_code: gst_number.substring(0, 2)
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditgst_state = resp.data.gst_state;
                    });
                }

                $scope.update_gst = function () {

                    var params = {
                        gststate_name: $scope.txteditgst_state,
                        gst_no: $scope.txteditgst_number,
                        gstregister_status: $scope.rdbgstregistered,
                        customer_gid: localStorage.getItem('customer_gid'),
                        customer2gst_gid: $scope.customer2gst_gid,
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GSTUpdate';
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
                        gst_templist();
                        $scope.txtgst_no = '';

                    });

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.emailaddress_edit = function (customer2emailaddress_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editemailaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_emailaddress: $scope.rdbeditprimary_emailaddress,
                        customer2emailaddress_gid: customer2emailaddress_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
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
                        emailaddress_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.mobileno_edit = function (customer2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2mobileno_gid: customer2mobileno_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/MobileNoEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimarymobile_no = resp.data.primary_mobileno;
                    $scope.rdbeditwhatsappmobile_no = resp.data.whatsapp_mobileno;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_mobileno: $scope.rdbeditprimarymobile_no,
                        whatsapp_mobileno: $scope.rdbeditwhatsappmobile_no,
                        customer2mobileno_gid: customer2mobileno_gid,
                        customer_gid: localStorage.getItem('customer_gid'),

                    }
                    var url = 'api/FndMstCustomerMasterAdd/MobileNoUpdate';
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
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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

                emailaddress_templist();
            });

        }

        $scope.Back = function () {
            $state.go('app.FndTrnCustomerApproval');
        }

        $scope.edit_Submit = function () {
            $state.go('app.FndTrnCustomerApproval');
        }
        $scope.viewcustomer = function (customer_gid, fndmanagement2cheque_gid) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndMstCustomerChequeView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + customer_gid + '&lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lstab=edit'));
        }
        $scope.editCheque = function (udcmanagement2cheque_gid) {
            $location.url('app/FndMstcustomerEditCheque?hash=' + cmnfunctionService.encryptURL('lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lsfndmanagement_gid=' + $scope.fndmanagement_gid + '&lstab=edit'));
        }



        $scope.customer_submit = function () {
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var lsamlcategory_gid = '';
            var lsamlcategory_name = '';
            var lsbusinesscategory_gid = '';
            var lsbusinesscategory_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
                lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
                lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
                lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            }
            if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
                lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
                lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            }


            var params = {
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                amlcategory_gid: lsamlcategory_gid,
                amlcategory_name: lsamlcategory_name,
                businesscategory_gid: lsbusinesscategory_gid,
                businesscategory_name: lsbusinesscategory_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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

        $scope.approver_update = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/approverEditUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnCustomerApproval');
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


        $scope.rejected_update = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/rejectedEditUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnCustomerApproval');
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



        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        //$scope.mobileno_edit = function () {

        //    if (($scope.mobile_no == undefined) || ($scope.mobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
        //        Notify.alert('Enter Mobile No/Select Status');
        //    }
        //    else {


        //        var params = {
        //            mobile_no: $scope.txtmobile_no,
        //            primary_mobileno: $scope.rdbprimarymobile_no,
        //            whatsapp_mobileno: $scope.rdbwhatsappmobile_no
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            customermobileno_list();
        //            $scope.txtmobile_no = '';
        //            $scope.rdbprimarymobile_no = '';
        //            $scope.rdbwhatsappmobile_no = '';
        //            $scope.rdbprimarymobile_no == false;
        //        });
        //    }
        //}

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (customer2mobileno_gid) {
            var params =
                {
                    customer2mobileno_gid: customer2mobileno_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteMobileNo';
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

                mobileno_templist();
            });

        }
        //function mobileno_templist() {
        //    var param = {
        //        customer_gid: $scope.customer_gid
        //    };
        //    var url = 'api/FndMstCustomerMasterAdd/MobileNoTempList';
        //    SocketService.getparams(url, param).then(function (resp) {
        //        $scope.mobileno_list = resp.data.customermobileno_list;
        //    });
        //}

        $scope.cheque_delete = function (fndmanagement2cheque_gid) {
            var params =
                {
                    fndmanagement2cheque_gid: fndmanagement2cheque_gid
                }
            //console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
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

                customermobileno_list();
            });

        }

        //$scope.emailaddress_edit = function (customer2emailaddress_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editemailaddress.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer2emailaddress_gid: customer2emailaddress_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.txteditemail_address = resp.data.email_address;
        //            $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update_emailaddress = function () {

        //            var params = {
        //                email_address: $scope.txteditemail_address,
        //                primary_emailaddress: $scope.rdbeditprimary_emailaddress,
        //                customer2emailaddress_gid: customer2emailaddress_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                emailaddress_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

        
        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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

                emailaddress_templist();
            });

        }

        //function emailaddress_templist() {
        //    var url = 'api/FndMstCustomerMasterAdd/EmailTempList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

        //    });
        //}
        function emailaddress_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

            });
        }

        $scope.address_edit = function (customer2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editaddressdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    customer2address_gid: customer2address_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/AddressDetailEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboaddresstype = resp.data.address_typegid;
                    $scope.rdbprimaryaddress = resp.data.primary_address;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2address_gid = resp.data.customer2address_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                    
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressUpdate = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboaddresstype,
                        address_type: address_type,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        primary_address: $scope.rdbprimaryaddress,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        customer2address_gid: $scope.customer2address_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/AddressDetailUpdate';
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
                        address_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }


        $scope.address_delete = function (customer2address_gid) {
            var params =
                {
                    customer2address_gid: customer2address_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteAddress';
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

                address_list();
            });

        }
        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    gst_list();
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';

                });
            }
        }
        $scope.emailaddress_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimaryemail_address,
                }
                var url = 'api/FndMstCustomerMasterAdd/PostEmailAddress';
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
                    emailaddress_templist();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        function emailaddress_templist() {
            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/EmailTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.customeremailaddress_list;

            });
        }


        function address_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;

            });
        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.edit_cheque = function () {
            var params = {
                stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                stakeholder_type: $scope.txtstakeholder_type,
                designation: $scope.txtdesignation,
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance
            }
            var url = 'api/UdcManagement/PostChequeDetail';
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
                cheque_list();

                $scope.cboStakeholder = '',
              $scope.txtstakeholder_type = '',
              $scope.txtdesignation = '',
              $scope.txtaccountholder_name = '',
              $scope.txtaccount_number = '',
              $scope.txtbank_name = "",
              $scope.txtcheque_no = "",
              $scope.txtifsc_code = "",
              $scope.txtmicr = "",
              $scope.txtbranch_address = "",
              $scope.txtbranch_name = "",
              $scope.txtcity = "",
              $scope.txtdistrict = "",
              $scope.txtstate = "",
              $scope.cbomergedbanking_entity = "",
              $scope.txtspecial_condition = "",
              $scope.txtgeneral_remarks = "",
              $scope.rbocts_enabled = "",
              $scope.cbocheque_type = "",
              $scope.txtdate_chequetype = "",
              $scope.txtdate_chequepresentation = "",
              $scope.txtstatus_chequepresentation = "",
              $scope.txtdate_chequeclearance = "",
              $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;


            });


        }
        function cheque_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
        }

        $scope.delete_cheque = function (udcmanagement2cheque_gid) {
            lockUI();
            var params = {
                udcmanagement2cheque_gid: udcmanagement2cheque_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }

        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}

        $scope.bank_delete = function (customer2bank_gid) {
            var params =
                {
                    customer2bank_gid: customer2bank_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteBank';
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

                bank_list();
            });

        }

        function bank_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetBankList';
            SocketService.get(url).then(function (resp) {
                $scope.customerbank_list = resp.data.customerbank_list;

            });
        }

        $scope.myraisequery = function (customer_gid) {
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
                    customer_gid: customer_gid
                }

                $scope.submit = function () {


                    var params = {
                        customer_gid: customer_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,

                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostCustomerRaiseQuery';
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
                            query_list(customer_gid);
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


        function query_list(customer_gid) {
            var params = {
                customer_gid: customer_gid,

            }

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerraisequery_list = resp.data.customerraisequery_list;
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

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnApprovalViewController', FndTrnApprovalViewController);

    FndTrnApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnApprovalViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnApprovalViewController';
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;        

        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };



            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountLevel';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountType';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            //SocketService.get(url).then(function (resp) {
            //    $scope.constitution_list = resp.data.constitution_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagency_list = resp.data.assessmentagency_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.amlcategory_list = resp.data.amlcategory_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.businesscategory_list = resp.data.businesscategory_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/state';
            //SocketService.get(url).then(function (resp) {
            //    $scope.state_list = resp.data.state_list;
            //});




            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;
            });



            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });




            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });



            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.customeraddress_list;
            });



            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_code = resp.data.customer_code;
                $scope.customer_name = resp.data.customer_name;
                $scope.businessstart_date = resp.data.businessstart_date;
                $scope.year_business = resp.data.year_business;
                $scope.month_business = resp.data.month_business;
                $scope.constitution_name = resp.data.constitution_name;
                $scope.assessmentagency_name = resp.data.assessmentagency_name;
                $scope.assessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.rating_date = resp.data.rating_date;
                $scope.amlcategory_name = resp.data.amlcategory_name;
                $scope.cin_no = resp.data.cin_no;
                $scope.pan_no = resp.data.pan_no;
                $scope.businesscategory_name = resp.data.businesscategory_name;
                $scope.remarks = resp.data.remarks;
                $scope.msme_registration = resp.data.msme_registration;
                $scope.contactperson_fn = resp.data.contactperson_fn;
                $scope.contactperson_mn = resp.data.contactperson_mn;
                $scope.contactperson_ln = resp.data.contactperson_ln;
                $scope.individualproof_name = resp.data.individualproof_name;
                $scope.designation_type = resp.data.designation_type;

                //if (resp.data.credit_status == 'Pending') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //} else if (resp.data.credit_status == 'Completed') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //}
                //else {
                //    $scope.showsubmit = true;
                //    $scope.showupdate = false;
                //}

                unlockUI();
            });

        }

        var params = {
            customer_gid: $scope.customer_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.cheque_list = resp.data.cheque_list;
        });


        //var params={
        //    customer_gid: $scope.customer_gid
        //}
        //var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        //SocketService.getparams(url, params).then(function (resp) {

        //    $scope.accountholder_name = resp.data.accountholder_name;
        //    $scope.account_number = resp.data.account_number;
        //    $scope.bank_name = resp.data.bank_name;
        //    $scope.cheque_no = resp.data.cheque_no;
        //    $scope.ifsc_code = resp.data.ifsc_code;
        //    $scope.micr = resp.data.micr;
        //    $scope.branch_address = resp.data.branch_address;
        //    $scope.branch_name = resp.data.branch_name;
        //    $scope.city = resp.data.city;
        //    $scope.district = resp.data.district;
        //    $scope.state = resp.data.state;
        //    $scope.mergedbankingentity_name = resp.data.mergedbankingentity_name;
        //    $scope.special_condition = resp.data.special_condition;
        //    $scope.general_remarks = resp.data.general_remarks;
        //    $scope.cts_enabled = resp.data.cts_enabled;
        //    $scope.cheque_type = resp.data.cheque_type;
        //    $scope.date_chequetype = resp.data.date_chequetype;
        //    $scope.date_chequepresentation = resp.data.date_chequepresentation;
        //    $scope.status_chequepresentation = resp.data.status_chequepresentation;
        //    $scope.date_chequeclearance = resp.data.date_chequeclearance;
        //    $scope.status_chequeclearance = resp.data.status_chequeclearance;

        //});

        //var url = 'api/FndMstCustomerMasterAdd/GetChequeDetails';
        //SocketService.get(url).then(function (resp) {
        //    $scope.cheque_list = resp.data.cheque_list;
        //});

        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.Back = function () {
            $state.go('app.FndTrnCustomerApproval');
        }

        //$scope.edit_Submit = function () {
        //    $state.go('app.FndMstCustomerMaster');
        //}




        //function gst_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customergst_list = resp.data.customergst_list;

        //    });
        //}








        //function emailaddress_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

        //    });
        //}





        //function address_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeraddress_list = resp.data.customeraddress_list;

        //    });
        //}

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        function cheque_list() {
            var url = 'api/UdcManagement/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
        }



        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}





    }
})();


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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalController', FndTrnCampaignApprovalController);

    FndTrnCampaignApprovalController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignApprovalController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignApprovalpending';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignApprovalCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignapprovalpending_count = resp.data.campaignapprovalpending_count;
                $scope.approvalrejected_count = resp.data.approvalrejected_count;
                $scope.approvalapproved_count = resp.data.approvalapproved_count;

            });
        }
        $scope.pendingcampaignapproval = function () {
            $state.go('app.FndTrnCampaignApproval');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignApprovalReject');
        }
     
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignApprovalWork');
        }


        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
      


        $scope.edit = function (val) {

            $location.url('app/FndTrnCampaignApprovalEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.viewcustomer = function (val) {

            $location.url('app/FndTrnCampaignApprovalView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }


        $scope.deletecampaign = function (campaign_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalEditController', FndTrnCampaignApprovalEditController);

    FndTrnCampaignApprovalEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCampaignApprovalEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalEditController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
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

            var params = {
                campaign_gid: $scope.campaign_gid
            }
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

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }
        $scope.view_querydesc = function (query_description, queryresponse_remarks,query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
        $scope.approved_update = function () {
            var params = {

                campaign_name: $scope.campaign_name,
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
                os_assesment_date: $scope.os_assesment_date,
                loan_availed: $scope.loan_availed,
                campaign_gid: $scope.campaign_gid,
                employees: $scope.employees,
                campaignapproval_remarks: $scope.remarks,


                         
            }
            var url = 'api/FndTrnCampaign/PostCampaignApproved';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnCampaignApproval');
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

        $scope.rejected_update = function () {
            var params = {

                campaign_name: $scope.campaign_name,
                campaigntype_gid: $scope.cboCampaign_type.campaigntype_gid,
                customer_gid: $scope.cboCustomer.customer_gid,
                contact_name: $scope.contact_name,
                contact_mobile: $scope.contact_mobile,
                contact_email: $scope.contact_email,
                assignee: $scope.cboCampaign_mgr,
                campaign_approver: $scope.cboCampaign_apr.employee_gid,
                campaign_cost: $scope.campaign_cost,
                start_date: $scope.start_date,
                end_date: $scope.end_date,
                assesment_date: $scope.assesment_date,
                os_assesment_date: $scope.os_assesment_date,
                loan_availed: $scope.loan_availed,
                campaign_gid: $scope.campaign_gid,
                campaignapproval_remarks: $scope.remarks,

            }


          
           var url = 'api/FndTrnCampaign/PostCampaignRejected';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndTrnCampaignApproval');
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

        $scope.raisequery = function (campaign_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
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
                $scope.submit = function () {
                    var params = {
                        campaign_gid: campaign_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description
                    }
                    var url = 'api/FndTrnCampaign/Postraisequery';
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


        function address_list(campaign_gid) {
            var params = {
                campaign_gid: campaign_gid,

            }

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });
        }

           
        $scope.Back = function () {
            $state.go('app.FndTrnCampaignApproval');
        }

        $scope.getQuestionnarie = function (cboSCategory) {
            var params = {
                categorytype_gid: $scope.cboSCategory.categorytype_gid,

            }

            lockUI();
            var url = 'api/FndTrnCampaign/GetQuestionnarie';
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
            var url = 'api/FndTrnCampaign/GetQuestionnarie';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.questionnarielist = resp.data.questionnarie_list;
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalRejectController', FndTrnCampaignApprovalRejectController);

    FndTrnCampaignApprovalRejectController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignApprovalRejectController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalRejectController';

        activate();
       
        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignRejected';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignApprovalCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignapprovalpending_count = resp.data.campaignapprovalpending_count;
                $scope.approvalrejected_count = resp.data.approvalrejected_count;
                $scope.approvalapproved_count = resp.data.approvalapproved_count;

            });

        }
        $scope.pendingcampaignapproval = function () {
            $state.go('app.FndTrnCampaignApproval');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignApprovalReject');
        }

        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignApprovalWork');
        }
        $scope.viewrejected = function (val) {

            $location.url('app/FndTrnCampaignApprovalRejectedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalRejectedViewController', FndTrnCampaignApprovalRejectedViewController);

    FndTrnCampaignApprovalRejectedViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCampaignApprovalRejectedViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalRejectedViewController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var lsQuestionnarie_gid;
        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });
            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }
        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignApprovalReject');
        }

    }

})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalViewController', FndTrnCampaignApprovalViewController);

    FndTrnCampaignApprovalViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCampaignApprovalViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalViewController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var lsQuestionnarie_gid;
        activate();

        function activate() {
                                 
            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;              
                $scope.cboCampaign_apr = resp.data.campaign_approver,              
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,
                

                unlockUI();
            });
            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }
        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignApproval');
        }
                        
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalWorkController', FndTrnCampaignApprovalWorkController);

    FndTrnCampaignApprovalWorkController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignApprovalWorkController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalWorkController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignApproval';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignApprovalCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignapprovalpending_count = resp.data.campaignapprovalpending_count;
                $scope.approvalrejected_count = resp.data.approvalrejected_count;
                $scope.approvalapproved_count = resp.data.approvalapproved_count;

            });
        }
        $scope.pendingcampaignapproval = function () {
            $state.go('app.FndTrnCampaignApproval');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignApprovalReject');
        }

        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignApprovalWork');
        }
        $scope.viewapprovalwork = function (val) {
            $location.url('app/FndTrnCampaignApprovalWorkView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalWorkViewController', FndTrnCampaignApprovalWorkViewController);

    FndTrnCampaignApprovalWorkViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCampaignApprovalWorkViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalWorkViewController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var lsScampaign_gid;
        var lsMcampaign_gid;
        var lscampaign_gid;
        var lsQuestionnarie_gid;
        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });
            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }
        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignApprovalWork');
        }

    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignClosedController', FndTrnCampaignClosedController);

    FndTrnCampaignClosedController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignClosedController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignClosedController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignClosed';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignpending_count = resp.data.campaignpending_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.approved_count = resp.data.approved_count;
                $scope.closed_count = resp.data.closed_count;

            });
        }

        $scope.Create = function () {
            $state.go('app.FndTrnCampaignAdd');
        }
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignWork');
        }
        $scope.Closed = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignReject');
        }
        $scope.Pending = function () {
            $state.go('app.FndTrnCampaignSummary');
        }
        $scope.viewcampaignclosed = function (val) {
            $location.url('app/FndTrnCampaignClosedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
        $scope.edit = function (val) {
            localStorage.setItem('campaign_gid', val);
            $state.go('app.FndTrnCampaignEdit');
        }

        $scope.deletecampaign = function (campaign_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignClosedViewController', FndTrnCampaignClosedViewController);

    FndTrnCampaignClosedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnCampaignClosedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignClosedViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }

        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignClosed');
        }
    }

})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignRejectController', FndTrnCampaignRejectController);

    FndTrnCampaignRejectController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignRejectController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignRejectController';

        activate();
       
        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignApprovalRejected';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignpending_count = resp.data.campaignpending_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.approved_count = resp.data.approved_count;
                $scope.closed_count = resp.data.closed_count;

            });
        }
        $scope.Create = function () {
            $state.go('app.FndTrnCampaignAdd');
        }
      
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignWork');
        }
        $scope.Closed = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignReject');
        }
        $scope.Pending = function () {
            $state.go('app.FndTrnCampaignSummary');
        }
        $scope.viewrejected = function (val) {
            $location.url('app/FndTrnCampaignRejectedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
        $scope.edit = function (val) {
            localStorage.setItem('campaign_gid', val);
            $state.go('app.FndTrnCampaignEdit');
        }

        $scope.deletecampaign = function (campaign_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignRejectedViewController', FndTrnCampaignRejectedViewController);

    FndTrnCampaignRejectedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnCampaignRejectedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignRejectedViewController';
        
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }

        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignReject');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignSummaryController', FndTrnCampaignSummaryController);

    FndTrnCampaignSummaryController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'SweetAlert','cmnfunctionService'];

    function FndTrnCampaignSummaryController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,SweetAlert,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignSummaryController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignSummary';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignpending_count = resp.data.campaignpending_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.approved_count = resp.data.approved_count;
                $scope.closed_count = resp.data.closed_count;
               
            });

        }
        $scope.Create = function () {
            $state.go('app.FndTrnCampaignAdd');
        }
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignWork');
        }
        $scope.Closed = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignReject');
        }
        $scope.Pending = function () {
            $state.go('app.FndTrnCampaignPendingSummary');
        }
        $scope.edit = function (val) {
           
            $location.url('app/FndTrnCampaignEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.view = function (val) {

            $location.url('app/FndTrnCampaignView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.delete = function (val) {
            var params = {
                campaign_gid: val
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {

                    var url = 'api/FndTrnCampaign/DeleteCampaign';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                           
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
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
      
        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
     

      
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignViewController', FndTrnCampaignViewController);

    FndTrnCampaignViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnCampaignViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = $scope.campaign_gid;

        activate();

        function activate() {
            
            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }

        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignWorkController', FndTrnCampaignWorkController);

    FndTrnCampaignWorkController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignWorkController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignWorkController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignApprovalApproved';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignpending_count = resp.data.campaignpending_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.approved_count = resp.data.approved_count;
                $scope.closed_count = resp.data.closed_count;

            });
        }
        $scope.Create = function () {
            $state.go('app.FndTrnCampaignAdd');
        }
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignWork');
        }
        $scope.Closed = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignReject');
        }
        $scope.Pending = function () {
            $state.go('app.FndTrnCampaignSummary');
        }
        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
        $scope.edit = function (val) {
            localStorage.setItem('campaign_gid', val);
            $state.go('app.FndTrnCampaignEdit');
        }
        $scope.viewcampiagnwork = function (val) {         
            $location.url('app/FndTrnCampaignWorkView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

        $scope.deletecampaign = function (campaign_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignWorkViewController', FndTrnCampaignWorkViewController);

    FndTrnCampaignWorkViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnCampaignWorkViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignWorkViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = $scope.campaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;


                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

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

        }

        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
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
            $state.go('app.FndTrnCampaignWork');
        }
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerApprovalController', FndTrnCustomerApprovalController);

    FndTrnCustomerApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerApprovalController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/GetcustomerApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });


             var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

           
            var url = 'api/FndMstCustomerMasterAdd/GetcustomerApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
      
            
            $scope.customersummary = function () {
                var url = 'api/FndMstCustomerMasterAdd/GetcustomerApprovalSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.customer_list = resp.data.customer_list;
                });
               
            }

         
            var url = 'api/FndMstCustomerMasterAdd/GetCustomerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpending_count = resp.data.customerpending_count;
                $scope.customerapproved_count = resp.data.customerapproved_count;
                $scope.customerrejected_count = resp.data.customerrejected_count;

            });
        }
        
        $scope.customersummary = function () {
            $state.go('app.FndTrnCustomerApproval');
        }
        $scope.approvecustomer = function () {
            $state.go('app.FndTrnCustomerApproved');
        }
        $scope.rejectcustomer = function () {
            $state.go('app.FndTrnCustomerRejected');
        }

        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}

        $scope.editcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterEdit');
            $location.url('app/FndTrnApprovalEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
        }

        $scope.viewcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndTrnApprovalView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
        }
        $scope.Status_update = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscustomer.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customer_gid = resp.data.customer_gid
                    $scope.txtcategory_type = resp.data.customer_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        customer_name: $scope.txtcategory_type,
                        customer_gid: $scope.customer_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    customer_gid: customer_gid
                }

                var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerinactivelog_data = resp.data.customer_list;
                    unlockUI();
                });
            }
        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerApprovedController', FndTrnCustomerApprovedController);

    FndTrnCustomerApprovedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerApprovedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerApprovedController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerapprover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerapprover_list = resp.data.customerapprover_list;
                unlockUI();

            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpending_count = resp.data.customerpending_count;
                $scope.customerapproved_count = resp.data.customerapproved_count;
                $scope.customerrejected_count = resp.data.customerrejected_count;

            });

        $scope.customersummary = function () {
            $state.go('app.FndTrnCustomerApproval');
        }
        $scope.approvecustomer = function () {
            $state.go('app.FndTrnCustomerApproved');
        }
        $scope.rejectcustomer = function () {
            $state.go('app.FndTrnCustomerRejected');
        }

        $scope.viewcustomer = function (val) {

            $location.url('app/FndTrnApprovalView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
        }

            $scope.approvecustomer = function () {

                var url = 'api/FndMstCustomerMasterAdd/Getcustomerapprover';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.customerapprover_list = resp.data.customerapprover_list;
                    unlockUI();

                });
            }
           


        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}

      


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerApprovedViewController', FndTrnCustomerApprovedViewController);

    FndTrnCustomerApprovedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerApprovedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerApprovedViewController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerapproverview';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerapprover_list = resp.data.customerapprover_list;
                unlockUI();

            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerViewCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpendingView_count = resp.data.customerpendingview_count;
                $scope.customerapprovedView_count = resp.data.customerapprovedview_count;
                $scope.customerrejectedView_count = resp.data.customerrejectedview_count;

            });

            ////$scope.customersummaryview = function () {
            ////    $state.go('app.FndMstCustomerMaster');
            ////}
            $scope.approvecustomerview = function () {
                $state.go('app.FndTrnCustomerApprovedView');
            }
            $scope.pendingcustomerview = function () {
                $state.go('app.FndMstCustomerMaster');
            }
            $scope.rejectcustomerview = function () {
                $state.go('app.FndTrnCustomerRejectedView');
            }
            $scope.addcustomer = function () {
                $state.go('app.FndMstCustomerMasterAdd');
            }
            $scope.viewcustomer = function (val) {

                $location.url('app/FndMstCustomerMasterView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
            }

            $scope.approvecustomerview = function () {

                var url = 'api/FndMstCustomerMasterAdd/Getcustomerapprover';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.customerapprover_list = resp.data.customerapprover_list;
                    unlockUI();

                });
            }



        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }

        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}




    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerRejectedController', FndTrnCustomerRejectedController);

    FndTrnCustomerRejectedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerRejectedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerRejectedController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerapprovalreject';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerrejected_list = resp.data.customerrejected_list;
                unlockUI();

            });


            var url = 'api/FndMstCustomerMasterAdd/GetCustomerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpending_count = resp.data.customerpending_count;
                $scope.customerapproved_count = resp.data.customerapproved_count;
                $scope.customerrejected_count = resp.data.customerrejected_count;

            });

            $scope.customersummary = function () {
                $state.go('app.FndTrnCustomerApproval');
            }
            $scope.approvecustomer = function () {
                $state.go('app.FndTrnCustomerApproved');
            }
            $scope.rejectcustomer = function () {
                $state.go('app.FndTrnCustomerRejected');
            }

            $scope.viewcustomer = function (val) {

                $location.url('app/FndTrnApprovalView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
            }


            $scope.rejectcustomer = function () {

                var url = 'api/FndMstCustomerMasterAdd/Getcustomerreject';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.customerrejected_list = resp.data.customerrejected_list;
                    unlockUI();

                });
            }




        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}




    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerRejectedViewController', FndTrnCustomerRejectedViewController);

    FndTrnCustomerRejectedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerRejectedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerRejectedViewController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerreject';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerrejected_list = resp.data.customerrejected_list;
                unlockUI();

            });
            var url = 'api/FndMstCustomerMasterAdd/GetCustomerViewCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpendingView_count = resp.data.customerpendingview_count;
                $scope.customerapprovedView_count = resp.data.customerapprovedview_count;
                $scope.customerrejectedView_count = resp.data.customerrejectedview_count;

            });


            $scope.approvecustomerview = function () {
                $state.go('app.FndTrnCustomerApprovedView');
            }
            $scope.rejectcustomerview = function () {
                $state.go('app.FndTrnCustomerRejectedView');
            }
            $scope.addcustomer = function () {
                $state.go('app.FndMstCustomerMasterAdd');
            }
            $scope.pendingcustomerview = function () {
                $state.go('app.FndMstCustomerMaster');
            }
            $scope.viewcustomer = function (val) {
               
                $location.url('app/FndMstCustomerMasterView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
            }
            $scope.rejectcustomerview = function () {

                var url = 'api/FndMstCustomerMasterAdd/Getcustomerreject';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.customerrejected_list = resp.data.customerrejected_list;
                    unlockUI();

                });
            }




        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}




    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalCloseController', FndTrnMyCampaignApprovalCloseController);

    FndTrnMyCampaignApprovalCloseController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignApprovalCloseController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalCloseController';

        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});

            var url = 'api/FndTrnMyCampaignSummary/GetCampaignApprovalclose';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.close_campaign = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignApprovalclose';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignSummaryCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignspending_count = resp.data.campaignspending_count;
                $scope.campaignsapproved_count = resp.data.campaignsapproved_count;
               
            });

        }

        //$scope.customersummary = function () {
        //    $state.go('app.FndTrnCustomerApproval');


        $scope.pending_campaign = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }
        $scope.close_campaign = function () {
            $state.go('app.FndTrnMyCampaignApprovalClosed');
        }
        $scope.showPopover = function (campaign_gid) {
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
                    campaign_gid: campaign_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                  
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.viewclosed = function (val) {          
            $location.url('app/FndTrnMyCampaignApprovalClosedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        ////$scope.addcustomer = function () {
        ////    $state.go('app.FndMstCustomerMasterAdd');
        ////}

        //$scope.editcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterEdit');
        //    $location.url('app/FndTrnApprovalEdit?lscustomer_gid=' + val);
        //}

       
        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalClosedViewController', FndTrnMyCampaignApprovalClosedViewController);

    FndTrnMyCampaignApprovalClosedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnMyCampaignApprovalClosedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalClosedViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = $scope.campaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignApprovalRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });



            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
            defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApprovalClosed');
        }
       
        $scope.stripAddr = function (value) {
            return value;
        }

        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);


                }
                else {
                    $scope.SampleDynamicTabledata = "";



                }
            });
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


    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalPendingController', FndTrnMyCampaignApprovalPendingController);

    FndTrnMyCampaignApprovalPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignApprovalPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalPendingController';

        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});

            var url = 'api/FndTrnMyCampaignSummary/GetCampaignApprovalpending';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.pending_campaign = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignApprovalpending';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignSummaryCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignspending_count = resp.data.campaignspending_count;
                $scope.campaignsapproved_count = resp.data.campaignsapproved_count;

            });


        }
        $scope.viewFormdata = function (val) {

            $location.url('app/FndTrnMyCampaignApprovalView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        //$scope.customersummary = function () {
        //    $state.go('app.FndTrnCustomerApproval');
        $scope.mycampaign_approval = function (val) {
           
            $location.url('app/FndTrnMyCampaignApproval?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
       
        $scope.pending_campaign = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }
        $scope.close_campaign = function () {
            $state.go('app.FndTrnMyCampaignApprovalClosed');
        }
        $scope.showPopover = function (campaign_gid) {
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
                    campaign_gid: campaign_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        ////$scope.addcustomer = function () {
        ////    $state.go('app.FndMstCustomerMasterAdd');
        ////}

        //$scope.editcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterEdit');
        //    $location.url('app/FndTrnApprovalEdit?lscustomer_gid=' + val);
        //}

        //$scope.viewcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterView');
        //    $location.url('app/FndTrnApprovalView?lscustomer_gid=' + val);
        //}
        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalViewController', FndTrnMyCampaignApprovalViewController);

    FndTrnMyCampaignApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService','cmnfunctionService'];

    function FndTrnMyCampaignApprovalViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.campaign_gid = searchObject.lscampaign_gid;
        var campaign_gid = searchObject.lscampaign_gid;
        
        activate();
        function activate() {
           
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });
            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignApprovalRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });


            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
               defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }
        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicTabledata = "";
                    $scope.SampleDynamicdata = "";
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);
                 
                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);
             

                }
                else {
                    $scope.SampleDynamicTabledata = "";
                   
                 
                   
                }
            });
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
        //Divya
        $scope.ExportExcel = function () {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/ExportSingleMultipleFormDetails';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
               
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }
            });
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovedController', FndTrnMyCampaignApprovedController);

    FndTrnMyCampaignApprovedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignApprovedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovedController';

        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});

            var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummaryRejected';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.rejected_campaign = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummaryRejected';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }
            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.mycampaignpending_count = resp.data.mycampaignpending_count;
                $scope.campaignapproved_count = resp.data.campaignapproved_count;
                $scope.campaignrejected_count = resp.data.campaignrejected_count;

            });


        }
        $scope.pending_campaign = function () {
            $state.go('app.FndTrnMyCampaignOpen');
        }
        $scope.approved_campaign = function () {
            $state.go('app.FndTrnMyCampaignPending');
        }
        $scope.rejected_campaign = function () {
            $state.go('app.FndTrnMyCampaignApproved');
        }
        $scope.viewrejected = function (val) {
            $location.url('app/FndTrnMyCampaignRejectedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.showPopover = function (campaign_gid) {
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
                    campaign_gid: campaign_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        ////$scope.addcustomer = function () {
        ////    $state.go('app.FndMstCustomerMasterAdd');
        ////}

        //$scope.editcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterEdit');
        //    $location.url('app/FndTrnApprovalEdit?lscustomer_gid=' + val);
        //}

        //$scope.viewcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterView');
        //    $location.url('app/FndTrnApprovalView?lscustomer_gid=' + val);
        //}
        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovedViewController', FndTrnMyCampaignApprovedViewController);

    FndTrnMyCampaignApprovedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnMyCampaignApprovedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovedViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = $scope.campaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });



            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
            defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignPending');
        }

        $scope.stripAddr = function (value) {
            return value;
        }

        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);


                }
                else {
                    $scope.SampleDynamicTabledata = "";



                }
            });
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
        //Divya
        $scope.ExportExcel = function () {

            var url = 'api/FndTrnMyCampaignSummary/ExportSingleMultipleFormDetails';
            lockUI();
            SocketService.post(url).then(function (resp) {
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
            });
        }

    }
})();

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

(function () {
    'use strict';
    angular
           .module('angle')
           .controller('FndTrnMyCampaignOpenController', FndTrnMyCampaignOpenController);
    
    FndTrnMyCampaignOpenController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignOpenController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignOpenController';
      
   
        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});
           
            var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummary';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.pending_campaign = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.mycampaignpending_count = resp.data.mycampaignpending_count;
                $scope.campaignapproved_count = resp.data.campaignapproved_count;
                $scope.campaignrejected_count = resp.data.campaignrejected_count;
              
            });

        }

        $scope.pending_campaign = function () {
            $state.go('app.FndTrnMyCampaignOpen');
        }
        $scope.approved_campaign = function () {
            $state.go('app.FndTrnMyCampaignPending');
        }
        $scope.rejected_campaign = function () {
            $state.go('app.FndTrnMyCampaignApproved');
        }

      

        $scope.editcampaign = function (val) {
          
            $location.url('app/FndTrnMyCampaignEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

        $scope.viewcampaign = function (val) {
           
            $location.url('app/FndTrnMyCampaignView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.showPopover = function (campaign_gid) {
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
                    campaign_gid: campaign_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignPendingController', FndTrnMyCampaignPendingController);

    FndTrnMyCampaignPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignPendingController';

        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});

            var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummaryApproved';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.approved_campaign = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummaryApproved';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }
            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.mycampaignpending_count = resp.data.mycampaignpending_count;
                $scope.campaignapproved_count = resp.data.campaignapproved_count;
                $scope.campaignrejected_count = resp.data.campaignrejected_count;

            });


        }

        //$scope.customersummary = function () {
        //    $state.go('app.FndTrnCustomerApproval');
        
        $scope.pending_campaign = function () {
            $state.go('app.FndTrnMyCampaignOpen');
        }
        $scope.approved_campaign = function () {
            $state.go('app.FndTrnMyCampaignPending');
        }
        $scope.rejected_campaign = function () {
            $state.go('app.FndTrnMyCampaignApproved');
        }
        $scope.viewapproved = function (val) {
            $location.url('app/FndTrnMyCampaignApprovedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.showPopover = function (campaign_gid) {
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
                    campaign_gid: campaign_gid
                }
                lockUI();
                var url = 'api/FndTrnMyCampaignSummary/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        ////$scope.addcustomer = function () {
        ////    $state.go('app.FndMstCustomerMasterAdd');
        ////}

        //$scope.editcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterEdit');
        //    $location.url('app/FndTrnApprovalEdit?lscustomer_gid=' + val);
        //}

        //$scope.viewcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterView');
        //    $location.url('app/FndTrnApprovalView?lscustomer_gid=' + val);
        //}
        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignRejectedViewController', FndTrnMyCampaignRejectedViewController);

    FndTrnMyCampaignRejectedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnMyCampaignRejectedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignRejectedViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;
        var campaign_gid = $scope.campaign_gid ;

        activate();

        function activate() {

            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });



            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
            defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApproved');
        }
        $scope.stripAddr = function (value) {
            return value;
        }

        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);


                }
                else {
                    $scope.SampleDynamicTabledata = "";



                }
            });
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


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignSummaryController', FndTrnMyCampaignSummaryController);

    FndTrnMyCampaignSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnMyCampaignSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignSummaryController';

        activate();

        function activate() {

            //var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            //SocketService.get(url).then(function (resp) {
            //});


            var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummary';
            SocketService.get(url).then(function (resp) {
                $scope.mycampaign_list = resp.data.mycampaign_list;
            });


            $scope.customersummary = function () {
                var url = 'api/FndTrnMyCampaignSummary/GetCampaignSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.mycampaign_list = resp.data.mycampaign_list;
                });

            }



        }
        $scope.edit = function (val) {

            $location.url('app/FndTrnMyCampaignEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        //$scope.customersummary = function () {
        //    $state.go('app.FndTrnCustomerApproval');
        //}
        //$scope.approvecustomer = function () {
        //    $state.go('app.FndTrnCustomerApproved');
        //}
        //$scope.rejectcustomer = function () {
        //    $state.go('app.FndTrnCustomerRejected');
        //}

        ////$scope.addcustomer = function () {
        ////    $state.go('app.FndMstCustomerMasterAdd');
        ////}

        //$scope.editcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterEdit');
        //    $location.url('app/FndTrnApprovalEdit?lscustomer_gid=' + val);
        //}

        //$scope.viewcustomer = function (val) {
        //    //localStorage.setItem('customer_gid', val);
        //    //$state.go('app.FndMstCustomerMasterView');
        //    $location.url('app/FndTrnApprovalView?lscustomer_gid=' + val);
        //}
        //$scope.Status_update = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/statuscustomer.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customer_gid = resp.data.customer_gid
        //            $scope.txtcategory_type = resp.data.customer_name;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.update_status = function () {

        //            var params = {
        //                customer_name: $scope.txtcategory_type,
        //                customer_gid: $scope.customer_gid,
        //                remarks: $scope.txtremarks,
        //                rbo_status: $scope.rbo_status

        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                activate();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //        var param = {
        //            customer_gid: customer_gid
        //        }

        //        var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
        //        lockUI();
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.customerinactivelog_data = resp.data.customer_list;
        //            unlockUI();
        //        });
        //    }
        //}
        //$scope.showsPopover = function (customer_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showremarks.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer_gid: customer_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtcustomer_code = resp.data.customer_code;
        //            $scope.txtcustomer_name = resp.data.customer_name;
        //            $scope.txteditremarks = resp.data.remarks;
        //            $scope.rbo_status = resp.data.Status;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };


        //    }
        //}
        //$scope.delete = function (customer_gid) {
        //    var params = {
        //        customer_gid: customer_gid
        //    }
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Record ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
        //            SocketService.getparams(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert('Error Occurred While Deleting Customer!', {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }

        //    });
        //};


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignViewController', FndTrnMyCampaignViewController);

    FndTrnMyCampaignViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnMyCampaignViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = $scope.campaign_gid;
        
        activate();

        function activate() {
          
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });

            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });



            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
               defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignOpen');
        }
        $scope.stripAddr = function (value) {
            return value;
        }

        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);

                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);


                }
                else {
                    $scope.SampleDynamicTabledata = "";



                }
            });
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


    }
})();
