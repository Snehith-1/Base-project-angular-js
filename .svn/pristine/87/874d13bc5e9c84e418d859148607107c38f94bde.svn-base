(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVerticalRulecontroller', MstVerticalRulecontroller);

    MstVerticalRulecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstVerticalRulecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVerticalRulecontroller';
        var vertical_gid = $location.search().lsvertical_gid;
        var verticalapplicanttyperule_gid = $location.search().lsverticalapplicanttyperule_gid;
        var applicant_type = $location.search().lsapplicant_type;

        activate();

        function activate() {
            $scope.editquesorder = true;

            $scope.answertype = [
                { id: 0, answer_typename: 'Calculation' },
                { id: 1, answer_typename: 'List' },
                { id: 2, answer_typename: 'Number' },
                { id: 3, answer_typename: '' }
            ]

            $scope.applicant_typelist = [
                { id: 0, applicant_type: 'Company' },
                { id: 1, applicant_type: 'Individual' },
                { id: 2, applicant_type: 'Group' },
                { id: 3, applicant_type: '' }
            ]
            

            var params = {
                verticalapplicanttyperule_gid: verticalapplicanttyperule_gid,
            }
            lockUI();
            var url = 'api/MstCreditMapping/GetVerticalGroupTitleListAppTypeEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_code = resp.data.vertical_code;
                $scope.vertical_name = resp.data.vertical_name;
                // $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                $scope.cboapplicant_type = resp.data.applicant_type;
                
            });

            var params = {
                vertical_gid: vertical_gid
            }
            var url = 'api/MstCreditMapping/GetVerticalGroupTitleList';
            SocketService.getparams(url, params).then(function (resp) {
                // $scope.vertical_code = resp.data.vertical_code;
                // $scope.vertical_name = resp.data.vertical_name;
                $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                // $scope.cboapplicant_type = resp.data.cboapplicant_type;
                unlockUI();
            });
            GetVerticalquestionsummarylist();
            
        }

        $scope.AnswerTypeChange = function (data) {
            if (data.answer_typename == 'Number' || data == null || data == "") {
                $scope.showlistdiv = false;
                $scope.shownumberdiv = true;
                $scope.showcalculatediv = false;
            }
            else if (data.answer_typename == 'List') {
                $scope.showlistdiv = true;
                $scope.listnamearr = [];
                $scope.shownumberdiv = false;
                $scope.showcalculatediv = false;

            }
            else if (data.answer_typename == 'Calculation') {
                $scope.showcalculatediv = true;
                $scope.showlistdiv = false;
                $scope.shownumberdiv = false;
                $scope.fieldtypelist = [
                    { id: 0, field_type: 'Score value', value: 'Score' },
                    { id: 1, field_type: 'Actual value', value: 'Actual' },
                    { id: 2, field_type: 'Constant value', value: 'Constant' },
                    { id: 3, field_type: '' }
                ]

                $scope.operationlist = [
                    { id: 0, operation_name: 'Addition' },
                    { id: 1, operation_name: 'Subtraction' },
                    { id: 2, operation_name: 'Division' },
                    { id: 3, operation_name: 'Multiplication' },
                    { id: 4, operation_name: 'Min' },
                    { id: 5, operation_name: 'Max' },
                    { id: 6, operation_name: 'IF' },
                    { id: 7, operation_name: 'Greater than' },
                    { id: 9, operation_name: 'Less than' },
                    { id: 11, operation_name: 'Equal to' },
                    { id: 12, operation_name: 'End' },
                    { id: 13, operation_name: '' }
                ]
                $scope.Calculationdetaillist = [];
            }
            else {
                $scope.showlistdiv = false;
                $scope.shownumberdiv = false;
                $scope.showcalculatediv = false;
            }
        }

        $scope.Addcalculation = function () {
            //Add the new item to the Array.
            var calculationdtl = {};
            calculationdtl.question = '';
            calculationdtl.field_type = '';
            calculationdtl.operations = '';
            calculationdtl.constantinput = true;
            $scope.Calculationdetaillist.push(calculationdtl);

        };

        $scope.Removecalculation = function (index) {
            $scope.Calculationdetaillist.splice(index, 1);
            var result = "";
            $scope.txtcalculation_formula = "((";
            $scope.simplify_formula = "";
            angular.forEach($scope.Calculationdetaillist, function (value, key) {
                if (value.field_type.field_type == undefined)
                    $scope.txtcalculation_formula += "" + value.question.question + "";
                else
                    $scope.txtcalculation_formula += "" + value.question.question + "(" + value.field_type.field_type + ")";
                result = $scope.simplify_formula.includes("Q" + key);
                if (result == false)
                    $scope.simplify_formula += "(Q" + key;
                value.simplify_key = "Q" + key;
                switch (value.operations.operation_name) {
                    case "Addition":
                        $scope.txtcalculation_formula += " ) + "
                        if (result == false)
                            $scope.simplify_formula += ") + ";
                        break;
                    case "Subtraction":
                        $scope.txtcalculation_formula += " ) - "
                        if (result == false)
                            $scope.simplify_formula += ") - ";
                        break;
                    case "Division":
                        $scope.txtcalculation_formula += " ) / "
                        if (result == false)
                            $scope.simplify_formula += ") / ";
                        break;
                    case "Multiplication":
                        $scope.txtcalculation_formula += " ) * ";
                        if (result == false)
                            $scope.simplify_formula += ") * ";
                        break;
                    case "Min":
                        $scope.txtcalculation_formula += " ) MIN ";
                        if (result == false)
                            $scope.simplify_formula += ") MIN ";
                        break;
                    case "Max":
                        $scope.txtcalculation_formula += " ) MAX ";
                        if (result == false)
                            $scope.simplify_formula += ") MAX ";
                        break;
                    case "IF":
                        $scope.txtcalculation_formula += " ) IF ";
                        if (result == false)
                            $scope.simplify_formula += ") IF ";
                        break;
                    case "Greater than":
                        $scope.txtcalculation_formula += " ) > ";
                        if (result == false)
                            $scope.simplify_formula += ") > ";
                        break;
                    case "Less than":
                        $scope.txtcalculation_formula += " ) < ";
                        if (result == false)
                            $scope.simplify_formula += ") < ";
                        break;
                    case "Equal to":
                        $scope.txtcalculation_formula += " ) = ";
                        if (result == false)
                            $scope.simplify_formula += ") = ";
                        break;
                    default:
                }
                if (value.field_type.value == "Constant" && $scope.constantvalue != undefined) {
                    $scope.txtcalculation_formula += "" + $scope.constantvalue + "";
                    $scope.simplify_formula += "" + $scope.constantvalue + "";
                }
            });
            $scope.txtcalculation_formula += ")";
            if (result == false)
                $scope.simplify_formula += ")";
        }

        $scope.changefieldtype = function (index) {
            if ($scope.Calculationdetaillist[index].field_type.value == "Constant")
                $scope.Calculationdetaillist[index].constantinput = false;
            else
                $scope.Calculationdetaillist[index].constantinput = true;
                $scope.Calculationdetaillist[index].constantvalue = "";
        }


        $scope.Changeformula = function () {
            var result = "";
            $scope.txtcalculation_formula = "((";
            $scope.simplify_formula = "";
            angular.forEach($scope.Calculationdetaillist, function (value, key) {
                if (value.field_type.field_type == undefined)
                    $scope.txtcalculation_formula += "" + value.question.question + "";
                else
                    $scope.txtcalculation_formula += "" + value.question.question + "(" + value.field_type.field_type + ")";
                result = $scope.simplify_formula.includes("Q" + key);
                if (result == false)
                    $scope.simplify_formula += "(Q" + key;
                value.simplify_key = "Q" + key;
                switch (value.operations.operation_name) {
                    case "Addition":
                        $scope.txtcalculation_formula += " ) + "
                        if (result == false)
                            $scope.simplify_formula += ") + ";
                        break;
                    case "Subtraction":
                        $scope.txtcalculation_formula += " ) - "
                        if (result == false)
                            $scope.simplify_formula += ") - ";
                        break;
                    case "Division":
                        $scope.txtcalculation_formula += " ) / "
                        if (result == false)
                            $scope.simplify_formula += ") / ";
                        break;
                    case "Multiplication":
                        $scope.txtcalculation_formula += " ) * ";
                        if (result == false)
                            $scope.simplify_formula += ") * ";
                        break;
                    case "Min":
                        $scope.txtcalculation_formula += " ) MIN ";
                        if (result == false)
                            $scope.simplify_formula += ") MIN ";
                        break;
                    case "Max":
                        $scope.txtcalculation_formula += " ) MAX ";
                        if (result == false)
                            $scope.simplify_formula += ") MAX ";
                        break;
                    case "IF":
                        $scope.txtcalculation_formula += " ) IF ";
                        if (result == false)
                            $scope.simplify_formula += ") IF ";
                        break;
                    case "Greater than":
                        $scope.txtcalculation_formula += " ) > ";
                        if (result == false)
                            $scope.simplify_formula += ") > ";
                        break;
                    case "Less than":
                        $scope.txtcalculation_formula += " ) < ";
                        if (result == false)
                            $scope.simplify_formula += ") < ";
                        break;
                    case "Equal to":
                        $scope.txtcalculation_formula += " ) = "
                        if (result == false)
                            $scope.simplify_formula += ") = ";
                        break;
                    default:
                }
                if (value.field_type.value == "Constant" && $scope.constantvalue != undefined) {
                    $scope.txtcalculation_formula += "" + $scope.constantvalue + "";
                    $scope.simplify_formula += "" + $scope.constantvalue + "";
                }
            });
            $scope.txtcalculation_formula += ")";
            if (result == false)
                $scope.simplify_formula += ")";
        }

        $scope.addquestion = function () {
            lockUI();
            var questionStatus = true;
            angular.forEach($scope.Calculationdetaillist, function (value, key) {
                
            if(value.question== '' || value.question == null || value.question == undefined){             
                questionStatus = false;    
        }
        else if(value.field_type== '' || value.field_type == null || value.field_type == undefined){
            questionStatus = false;
                 
        }
        else if(value.operations== '' || value.operations == null || value.operations == undefined){
            questionStatus = false;        
        }
        else if((value.question != '' || value.question != null || value.question != undefined) && (value.field_type!= '' || value.field_type != null || value.field_type != undefined)
        && (value.operations!= '' || value.operations != null || value.operations != undefined)) {
                    value.grouptitle_gid = value.question.grouptitle_gid,
                    value.grouptitle_name = value.question.grouptitle_name,
                    value.question_gid = value.question.verticalquestionrule_gid,
                    value.question = value.question.question,
                    value.field_type = value.field_type.value,
                    value.operations = value.operations.operation_name
        }
            });

            if(questionStatus == false) {
                unlockUI();
                                Notify.alert("Kindly add All Question Details", {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
            }
            else {

            var params = {
                vertical_gid: vertical_gid,
                grouptitle_gid: $scope.cbogroup_title.grouptitle_gid,
                grouptitle_name: $scope.cbogroup_title.grouptitle_name,
                question: $scope.txtquestion,
                answer_type: $scope.cboanswer_type.answer_typename,
                listarray: $scope.listnamearr,
                number_score: $scope.txtNumberScore,
                calculation_dtl: $scope.Calculationdetaillist,
                calculation_formula: $scope.txtcalculation_formula,
                simplify_formula: $scope.simplify_formula,
                addfinal_score: $scope.rdbFinalScore,
                hidden_question: $scope.rdbhiddenquestion,
                // applicant_type: $scope.cboapplicant_type.applicant_type,
                // applicant_typegid: $scope.cboapplicant_type.id,
                applicant_type: applicant_type,
                verticalapplicanttyperule_gid: verticalapplicanttyperule_gid,

            }
            var url = 'api/MstCreditMapping/PostCreateVerticalRule';

            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.cbogroup_title = "";
                    $scope.cboanswer_type = "";
                    $scope.txtlistName = "";
                    $scope.txtScore = "";
                    $scope.listnamearr = "";
                    $scope.txtcalculation_formula = "";
                    $scope.txtNumberScore = "";
                    $scope.txtquestion = "";
                    $scope.showlistdiv = false;
                    $scope.shownumberdiv = false;
                    $scope.showcalculatediv = false;
                    $scope.Calculationdetaillist = [];
                    GetVerticalquestionsummarylist();
                    $scope.simplify_formula = "";
                    $scope.rdbFinalScore = "";
                    $scope.rdbhiddenquestion = "";
                    // $scope.cboapplicant_type = "";
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
            }
        }

        $scope.addlist = function () {
            //var id = 0;
            //if($scope.listnamearr && $scope.listnamearr.length != 0)
            //    id = $scope.listnamearr.length + 1;

            $scope.listnamearr.push({
                list_name: $scope.txtlistName,
                Score: $scope.txtScore
            });
            $scope.txtlistName = "";
            $scope.txtScore = "";
        }

        $scope.deletelist = function (index) {
            $scope.listnamearr.splice(index, 1);
        }

        function GetVerticalquestionsummarylist() {
            var params = {
                vertical_gid: vertical_gid,
                applicant_type: applicant_type,
            }
            var url = 'api/MstCreditMapping/GetVerticalquestionsummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlGroupTitleQuestionlist = resp.data.MdlVerticalGroupTitleQuestion;
                unlockUI();
            });
        }

        $scope.rule_view = function (grouptitle_name, question, verticalquestionrule_gid, answer_type, calculation_formula, addfinal_score, hidden_question, applicant_type) {
            var modalInstance = $modal.open({
                templateUrl: '/ViewRule.html',
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
                $scope.txtviewquestion = question;
                $scope.txtviewgrouptitle_name = grouptitle_name;
                $scope.txtapplicant_type = applicant_type;
                $scope.txtcalculation_formula = calculation_formula;
                $scope.txtanswer_type = answer_type;
                $scope.lbladdfinal_score = addfinal_score;
                $scope.lblhidden_question = hidden_question;
                var params = {
                    verticalquestionrule_gid: verticalquestionrule_gid
                }
                var url = 'api/MstCreditMapping/GetVerticalquestionlistsummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.listnamearrdtl = resp.data.listarray;
                    $scope.calculationlistarrdtl = resp.data.calculation_dtl;
                    unlockUI();
                });
            }
        }

        $scope.rule_delete = function (verticalquestionrule_gid) {
            var params = {
                verticalquestionrule_gid: verticalquestionrule_gid
            }
            var url = 'api/MstCreditMapping/GetDeleteVerticalQuestionList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    GetVerticalquestionsummarylist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.back = function () {
            $location.url('app/MstVerticalApplicantTypeRule?lsvertical_gid=' + vertical_gid);
        }
        // $scope.back = function () {
        //     $state.go('app.MstVerticalApplicantTypeRule');
        // };

        $scope.editquestionOrder = function () {
            $scope.editquesorder = false;
        }

        $scope.updatequestionOrder = function () {
            lockUI();
            $scope.editquesorder = true;
            var params = {
                MdlVerticalGroupTitleQuestion: $scope.MdlGroupTitleQuestionlist
            }
            var url = 'api/MstCreditMapping/UpdateVerticalGroupQuestionOrder';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetVerticalquestionsummarylist();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.CancelquestionOrder = function () {
            $scope.editquesorder = true;
            GetVerticalquestionsummarylist();
        }

        $scope.editgroupOrder = function () {
            var modalInstance = $modal.open({
                templateUrl: '/EditGroupOrderPopup.html',
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
                var params = {
                    vertical_gid: vertical_gid,
                    applicant_type: applicant_type,
                }
                var url = 'api/MstCreditMapping/GetVerticalGroupList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.MdlGroupTitle_list = resp.data.GroupTitle_dtl;
                    unlockUI();
                });

                $scope.update = function () {
                    var params = {
                        GroupTitle_dtl: $scope.MdlGroupTitle_list
                    }
                    var url = 'api/MstCreditMapping/UpdateVerticalGroupOrder';

                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            GetVerticalquestionsummarylist();
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

                    });
                }
            }
        }
    }
})();
