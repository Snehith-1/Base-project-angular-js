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
