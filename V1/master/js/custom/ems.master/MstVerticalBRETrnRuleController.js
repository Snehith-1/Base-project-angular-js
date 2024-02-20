(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVerticalBRETrnRuleController', MstVerticalBRETrnRuleController);

        MstVerticalBRETrnRuleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function MstVerticalBRETrnRuleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'MstVerticalBRETrnRuleController';
        $scope.vertical_gid = $location.search().lsvertical_gid;
        var vertical_gid = $scope.vertical_gid; 

        $scope.application_gid = $location.search().lsapplication_gid;
        var application_gid = $scope.application_gid; 
        $scope.institution_gid = $location.search().lsinstitution_gid;
        var institution_gid = $scope.institution_gid; 
        $scope.contact_gid = $location.search().lscontact_gid;
        var contact_gid = $scope.contact_gid; 
        $scope.group_gid = $location.search().lsgroup_gid;
        var group_gid = $scope.group_gid; 
        
        $scope.lscompany = $location.search().lscompany;
        var lscompany = $scope.lscompany;
        $scope.lsindividual = $location.search().lsindividual;
        var lsindividual = $scope.lsindividual;
        $scope.lsgroup = $location.search().lsgroup;
        var lsgroup = $scope.lsgroup;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspagename = $location.search().lspagename;
        var lspagename = $scope.lspagename;
        

        lockUI();
        activate();
        function activate() {     
            debugger;
            if(lscompany=='Company') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lscompany,
                    application_gid: application_gid,
                    editruletype_gid: institution_gid
                    
                    }
            }
            else if(lsindividual=='Individual') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsindividual,
                    application_gid: application_gid,
                    editruletype_gid: contact_gid

                    }
            }
            else if(lsgroup=='Group') {
                            
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsgroup,
                    application_gid: application_gid,
                    editruletype_gid: group_gid

                    }
                }
            else {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    application_gid: application_gid,
                    
                    }
                } 
                // var params = {
                //     vertical_gid: $scope.vertical_gid,
                //     }

            var url = 'api/MstCreditMapping/GetVerticalBasicView';
                debugger;
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();               
                $scope.txtvertical_name = resp.data.vertical_name;               
                if (resp.data.scorecard_submit == "0" || resp.data.scorecard_submit == "") {
                    lockUI();
                    $scope.showsubmitscoreevent = true;
                    var url = 'api/MstCreditMapping/GetVerticalScorecarddtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.Vertical_GroupTitle_dtl;
                        $scope.GroupQuestion_list1 = resp.data.MdlTrnVerticalGroupTitleQuestion;
                        $scope.grouplistdtl = resp.data.Vertical_listarray;

                        angular.forEach($scope.GroupQuestion_list1, function (value, key) {
                            if (value.verticalquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.verticalquestionrule_gid === value.verticalquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                }
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list1.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list1 = getGroupQuestionListArray;
                                }
                            }
                        });
                    });
                }
                else {
                    $scope.showsubmitscoreevent = false;
                    lockUI();
                    var url = 'api/MstCreditMapping/GetVerticalScorecardViewdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.Vertical_GroupTitle_dtl;
                        $scope.GroupQuestion_list1 = resp.data.MdlTrnVerticalGroupTitleQuestion;

                        $scope.grouplistdtl = resp.data.Vertical_listarray;

                        angular.forEach($scope.GroupQuestion_list1, function (value, key) {
                            if (value.verticalquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.verticalquestionrule_gid === value.verticalquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }

                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list1.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list1 = getGroupQuestionListArray;
                                }
                            }
                        });
                    });
                } 
                unlockUI();
            });  
            
        }

        $scope.Back = function () {
            // $location.url('app/AgrTrnStartCreditUnderwriting?lsapplication_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp'); // + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            if(lscompany=="Company")
                $location.url('app/'+ lspagename +'?application_gid=' + application_gid + '&lsinstitution_gid=' + institution_gid + '&lsvertical_gid=' + vertical_gid + '&lspage=' + lspage);
            else if(lsindividual=="Individual")
                $location.url('app/'+ lspagename +'?application_gid=' + application_gid + '&lscontact_gid=' + contact_gid + '&lsvertical_gid=' + vertical_gid + '&lspage=' + lspage);
            else if(lsgroup=="Group")   
                $location.url('app/'+ lspagename +'?application_gid=' + application_gid + '&lsgroup_gid=' + group_gid + '&lsvertical_gid=' + vertical_gid + '&lspage=' + lspage);
            else {
                $location.url('app/'+ lspagename +'?application_gid=' + application_gid + '&lsvertical_gid=' + vertical_gid + '&lspage=' + lspage);
            }
        }           

            
        $scope.changefieldtype = function (index, cbofield_type, DropdownListArray1,questionindex) {
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list1, function (value, key) {
                if (value.verticalquestionrule_gid == cbofield_type.verticalquestionrule_gid) {
                    if (cbofield_type.Score == 'Rejected') {
                        value.DropdownListArray1 = cbofield_type;
                        value.final_score = "0";
                        if (value.answer_type == "List")
                            value.Score = "Rejected";
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = "0";
                    }
                    else {
                        value.final_score = cbofield_type.Score;
                        value.DropdownListArray1 = [];
                        value.DropdownListArray1.push(cbofield_type);
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = cbofield_type.Score;
                        if (value.answer_type == "List")
                            value.Score = cbofield_type.Score;
                    }
                   
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list1.filter(function (el) { return el.verticalquestionrule_gid === cbofield_type.verticalquestionrule_gid });
            var lsverticalquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list1[questionindex].verticalquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                var params = {
                    verticalquestionrule_gid: lsverticalquestionrule_gid,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid, 
                }
                var url = 'api/MstCreditMapping/GetVerticalQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_list, function (value, key) { 
                            angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.verticalquestionrule_gid === value1.verticalquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            }); 
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                             sum = 0;
                            angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                    sum += parseFloat(value1.final_scoredisplay);
                            });
                            value.final_scoredisplay = parseFloat(sum).toFixed(2);
                        });
                        
                    }
                });
            }



        }

        $scope.ChangeNumberField = function (index, field_number, questionruleindex) {
            var verticalquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list1[questionruleindex].verticalquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list1, function (value, key) {

                if (value.verticalquestionrule_gid == verticalquestionrule_gid) {
                    value.actual_number = field_number;
                    value.final_score = field_number;
                    if (value.addfinal_score == "Yes")
                        value.final_scoredisplay = field_number;
                    if (value.answer_type == "List")
                        value.Score = field_number;
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list1.filter(function (el) { return el.verticalquestionrule_gid === verticalquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        verticalquestionrule_gid: verticalquestionrule_gid,
                        GroupTitle_list1: $scope.GroupTitle_list,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstCreditMapping/GetVerticalQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.verticalquestionrule_gid === value1.verticalquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                    if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                        sum += parseFloat(value1.final_scoredisplay);
                                });
                                value.final_scoredisplay = parseFloat(sum).toFixed(2);
                            });

                        } 
                    });
                }
            // } 
        }

        $scope.changefieldtypeEdit = function (index, cbofield_type, DropdownListArray1, questionindex) {
            $scope.btnupdateshow = true;
            var sum = 0;
            cbofield_type = DropdownListArray1.filter(function (el) { return el.verticalquestionlistoption_gid === cbofield_type });
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list1, function (value, key) {
                if (value.verticalquestionrule_gid == cbofield_type[0].verticalquestionrule_gid) {
                    if (cbofield_type[0].Score == 'Rejected') {
                        value.DropdownListArray1 = cbofield_type;
                        value.final_score = "0";
                        if (value.answer_type == "List") {
                            value.actual_score = "Rejected";
                            value.Score = "Rejected";
                        } 
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = "0";
                    }
                    else {
                        value.final_score = cbofield_type[0].Score;
                        value.DropdownListArray1 = [];
                        value.DropdownListArray1.push(cbofield_type[0]);
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = cbofield_type[0].Score;
                        if (value.answer_type == "List") {
                            value.actual_score = cbofield_type[0].Score;
                            value.Score = cbofield_type[0].Score;
                        } 
                    }

                }
                else {
                    if (value.cbofield_type != "" && value.cbofield_type.length != 0) {
                        value.DropdownListArray1 = [];
                        var getlist = value.DropdownListArraydtl.filter(function (el) { return el.verticalquestionlistoption_gid === value.cbofield_type });
                        value.DropdownListArray1 = getlist;
                    }
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list1.filter(function (el) { return el.verticalquestionrule_gid === cbofield_type[0].verticalquestionrule_gid });
            var lsverticalquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list1[questionindex].verticalquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                var params = {
                    verticalquestionrule_gid: lsverticalquestionrule_gid,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid,
                }
                var url = 'api/MstCreditMapping/GetVerticalQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.verticalquestionrule_gid === value1.verticalquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            });
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            sum = 0;
                            angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                    sum += parseFloat(value1.final_scoredisplay);
                            });
                            value.final_scoredisplay = parseFloat(sum).toFixed(2);
                        });

                    }
                });
            }



        }

        $scope.ChangeNumberFieldEdit = function (index, field_number, questionruleindex) {
            $scope.btnupdateshow = true;
            var verticalquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list1[questionruleindex].verticalquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list1, function (value, key) {

                if (value.verticalquestionrule_gid == verticalquestionrule_gid) {
                    value.actual_number = field_number;
                    value.final_score = field_number;
                    if (value.addfinal_score == "Yes")
                        value.final_scoredisplay = field_number;
                    if (value.answer_type == "List") {
                        value.actual_score = field_number;
                        value.Score = field_number;
                    } 
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list1.filter(function (el) { return el.verticalquestionrule_gid === verticalquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        verticalquestionrule_gid: verticalquestionrule_gid,
                        GroupTitle_list1: $scope.GroupTitle_list,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstCreditMapping/GetVerticalQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.verticalquestionrule_gid === value1.verticalquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list1, function (value1, key1) {
                                    if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                        sum += parseFloat(value1.final_scoredisplay);
                                });
                                value.final_scoredisplay = parseFloat(sum).toFixed(2);
                            });

                        }
                    });
                }
            // }


        }

        $scope.saveScoreCard = function () {
            angular.forEach($scope.GroupTitle_list, function (value, key) {
                var getgroupquestion = $scope.GroupTitle_list[key].GroupQuestion_list1;
                angular.forEach(getgroupquestion, function (value, key) {
                    if (value.cbofield_type != "" && value.cbofield_type.length != 0) {
                        value.DropdownListArray1 = [];
                        var getlist = value.DropdownListArraydtl.filter(function (el) { return el.verticalquestionlistoption_gid === value.cbofield_type });
                        value.DropdownListArray1 = getlist;
                    }  
                    value.Score = value.actual_score;
                });
            });
              
            if(lscompany=='Company') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lscompany,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: institution_gid
                    
                    }
            }
            else if(lsindividual=='Individual') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsindividual,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: contact_gid

                    }
            }
            else if(lsgroup=='Group') {
                            
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsgroup,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: group_gid

                    }
                }
            else {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,

                    }
                } 
                // var params = {
                //     vertical_gid: $scope.vertical_gid,
                //     }
            
            var url = 'api/MstCreditMapping/VerticalSaveScoreCard';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.btnupdateshow = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.showsubmitscoreevent = false;

                    if(lscompany=='Company') {
                        
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lscompany,
                            application_gid: application_gid,
                            editruletype_gid: institution_gid
                            
                            }
                    }
                    else if(lsindividual=='Individual') {
                                
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lsindividual,
                            application_gid: application_gid,
                            editruletype_gid: contact_gid
        
                            }
                    }
                    else if(lsgroup=='Group') {
                                    
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lsgroup,
                            application_gid: application_gid,
                            editruletype_gid: group_gid

                            }
                        }
                    else {
                                
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            application_gid: application_gid,
                            
                            }
                        } 
                        // var params = {
                        //     vertical_gid: $scope.vertical_gid,
                        //     }
                    
                    lockUI();
                    var url = 'api/MstCreditMapping/GetVerticalScorecardViewdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.Vertical_GroupTitle_dtl;
                        $scope.GroupQuestion_list1 = resp.data.MdlTrnVerticalGroupTitleQuestion;
 						$scope.grouplistdtl = resp.data.Vertical_listarray;

                        angular.forEach($scope.GroupQuestion_list1, function (value, key) {
                            if (value.verticalquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.verticalquestionrule_gid === value.verticalquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }

                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list1.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list1 = getGroupQuestionListArray;
                                }
                            }
                        });
                    });
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

        $scope.submitScoreCard = function () {

            if(lscompany=='Company') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lscompany,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: institution_gid
                    
                    }
            }
            else if(lsindividual=='Individual') {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsindividual,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: contact_gid
                    }
            }
            else if(lsgroup=='Group') {
                            
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    applicanttype: lsgroup,
                    GroupTitle_list1: $scope.GroupTitle_list,
                    application_gid: application_gid,
                    editruletype_gid: group_gid
                    }
                }
            else {
                        
                var params = {
                    vertical_gid: $scope.vertical_gid,
                    GroupTitle_list1: $scope.GroupTitle_list, 
                    application_gid: application_gid,
                    }
                } 
                // var params = {
                //     vertical_gid: $scope.vertical_gid,
                //     }
            
            var url = 'api/MstCreditMapping/VerticalSubmitScoreCard';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.showsubmitscoreevent = false;
                    
                    if(lscompany=='Company') {
                        
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lscompany,
                            application_gid: application_gid,
                            editruletype_gid: institution_gid
                            
                            }
                    }
                    else if(lsindividual=='Individual') {
                                
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lsindividual,
                            application_gid: application_gid,
                            editruletype_gid: contact_gid
        
                            }
                    }
                    else if(lsgroup=='Group') {
                                    
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            applicanttype: lsgroup,
                            application_gid: application_gid,
                            editruletype_gid: group_gid

                            }
                        }
                    else {
                                
                        var params = {
                            vertical_gid: $scope.vertical_gid,
                            application_gid: application_gid,
                            
                            }
                        } 
                        // var params = {
                        //     vertical_gid: $scope.vertical_gid,
                        //     }
                    lockUI();
                    var url = 'api/MstCreditMapping/GetVerticalScorecardViewdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.Vertical_GroupTitle_dtl;
                        $scope.GroupQuestion_list1 = resp.data.MdlTrnVerticalGroupTitleQuestion;
                        $scope.grouplistdtl = resp.data.Vertical_listarray;

                        angular.forEach($scope.GroupQuestion_list1, function (value, key) {
                            if (value.verticalquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.verticalquestionrule_gid === value.verticalquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }

                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list1.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list1 = getGroupQuestionListArray;
                                }
                            }
                        });
                    });
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
    }
        
})();
