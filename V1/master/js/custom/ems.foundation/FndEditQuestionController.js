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
