(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditColendingRuleViewController', MstCreditColendingRuleViewController);

        MstCreditColendingRuleViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditColendingRuleViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'MstCreditColendingRuleViewController';         
        var colendingprogram_gid = $location.search().lscolendingprogram_gid;
        
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;

        $scope.lscompany = $location.search().lscompany;
        var lscompany = $scope.lscompany;
        $scope.lsindividual = $location.search().lsindividual;
        var lsindividual = $scope.lsindividual;
        $scope.lsgroup = $location.search().lsgroup;
        var lsgroup = $scope.lsgroup;
        $scope.lspagename = $location.search().lspagename;
        var lspagename = $scope.lspagename;
        $scope.lspagetype = $location.search().lspagetype;
        var lspagetype = $scope.lspagetype;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lscolending_applicanttype = $location.search().lscolending_applicanttype;
        var lscolending_applicanttype = $scope.lscolending_applicanttype;

        $scope.colendingruleview_applicanttype = false;

        lockUI();
        activate();
        function activate() {  
        if(lscolending_applicanttype=="Company_Individual" && lscolending_applicanttype!=undefined && lscolending_applicanttype!=""){
            
            $scope.colendingruleview_applicanttype = true;

            applicant_type_company();
            applicant_type_individual();

            function applicant_type_company(){
                var params = {
                    colendingprogram_gid: colendingprogram_gid,
                    application_gid: application_gid,
                    applicant_type: "Company"               
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingBasicView';

                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();               
                    $scope.txtcolendar_name = resp.data.colendar_name;               
                    if (resp.data.scorecard_submit == "0" || resp.data.scorecard_submit == "") {
                        lockUI();
                        $scope.showsubmitscoreevent = true;
                        var url = 'api/MstAppCreditUnderWriting/GetColendingScorecarddtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.GroupTitle_List_Colending = resp.data.Colending_GroupTitle_dtl;
                            $scope.GroupQuestion_list_Colending = resp.data.MdlTrnColendingGroupTitleQuestion;
                            $scope.grouplistdtl = resp.data.Colending_listarray;

                            angular.forEach($scope.GroupQuestion_list_Colending, function (value, key) {
                                if (value.colendingquestionrule_gid != "") {
                                    var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                    if (getDropdownListArray != null) {
                                        value.DropdownListArraydtl = getDropdownListArray;
                                    }
                                }
                            });

                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                if (value.grouptitle_gid != "") {
                                    var getGroupQuestionListArray = $scope.GroupQuestion_list_Colending.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                    if (getGroupQuestionListArray != null) {
                                        value.GroupQuestion_list_Colending = getGroupQuestionListArray;
                                    }
                                }
                            });
                        });
                    }
                    else {
                        $scope.showsubmitscoreevent = false;
                        lockUI();
                        var url = 'api/MstAppCreditUnderWriting/GetColendingScorecardViewdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.GroupTitle_List_Colending = resp.data.Colending_GroupTitle_dtl;
                            $scope.GroupQuestion_list_Colending = resp.data.MdlTrnColendingGroupTitleQuestion;

                            $scope.grouplistdtl = resp.data.Colending_listarray;

                            angular.forEach($scope.GroupQuestion_list_Colending, function (value, key) {
                                if (value.colendingquestionrule_gid != "") {
                                    var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                    if (getDropdownListArray != null) {
                                        value.DropdownListArraydtl = getDropdownListArray;
                                        value.cbofield_type = value.actualvalue_gid;
                                    }

                                    value.field_number = value.actual_value;
                                }
                            });

                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                if (value.grouptitle_gid != "") {
                                    var getGroupQuestionListArray = $scope.GroupQuestion_list_Colending.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                    if (getGroupQuestionListArray != null) {
                                        value.GroupQuestion_list_Colending = getGroupQuestionListArray;
                                    }
                                }
                            });
                        });
                    }   
                });

            }

            function applicant_type_individual(){
                var params = {
                    colendingprogram_gid: colendingprogram_gid,
                    application_gid: application_gid,
                    applicant_type: "Individual"               
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingBasicView';

                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();               
                    $scope.txtcolendar_name = resp.data.colendar_name;               
                    if (resp.data.scorecard_submit == "0" || resp.data.scorecard_submit == "") {
                        lockUI();
                        $scope.showsubmitscoreevent = true;
                        var url = 'api/MstAppCreditUnderWriting/GetColendingScorecarddtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.GroupTitle_List_ColendingIndividual = resp.data.Colending_GroupTitle_dtl;
                            $scope.GroupQuestion_list_ColendingIndividual = resp.data.MdlTrnColendingGroupTitleQuestion;
                            $scope.grouplistdtlIndividual = resp.data.Colending_listarray;

                            angular.forEach($scope.GroupQuestion_list_ColendingIndividual, function (value, key) {
                                if (value.colendingquestionrule_gid != "") {
                                    var getDropdownListArray = $scope.grouplistdtlIndividual.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                    if (getDropdownListArray != null) {
                                        value.DropdownListArraydtl = getDropdownListArray;
                                    }
                                }
                            });

                            angular.forEach($scope.GroupTitle_List_ColendingIndividual, function (value, key) {
                                if (value.grouptitle_gid != "") {
                                    var getGroupQuestionListArray = $scope.GroupQuestion_list_ColendingIndividual.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                    if (getGroupQuestionListArray != null) {
                                        value.GroupQuestion_list_ColendingIndividual = getGroupQuestionListArray;
                                    }
                                }
                            });
                        });
                    }
                    else {
                        $scope.showsubmitscoreevent = false;
                        lockUI();
                        var url = 'api/MstAppCreditUnderWriting/GetColendingScorecardViewdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.GroupTitle_List_ColendingIndividual = resp.data.Colending_GroupTitle_dtl;
                            $scope.GroupQuestion_list_ColendingIndividual = resp.data.MdlTrnColendingGroupTitleQuestion;
                            $scope.grouplistdtlIndividual = resp.data.Colending_listarray;

                            angular.forEach($scope.GroupQuestion_list_ColendingIndividual, function (value, key) {
                                if (value.colendingquestionrule_gid != "") {
                                    var getDropdownListArray = $scope.grouplistdtlIndividual.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                    if (getDropdownListArray != null) {
                                        value.DropdownListArraydtl = getDropdownListArray;
                                        value.cbofield_type = value.actualvalue_gid;
                                    }

                                    value.field_number = value.actual_value;
                                }
                            });

                            angular.forEach($scope.GroupTitle_List_ColendingIndividual, function (value, key) {
                                if (value.grouptitle_gid != "") {
                                    var getGroupQuestionListArray = $scope.GroupQuestion_list_ColendingIndividual.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                    if (getGroupQuestionListArray != null) {
                                        value.GroupQuestion_list_ColendingIndividual = getGroupQuestionListArray;
                                    }
                                }
                            });
                        });
                    }
                    
                });

            }
            
            unlockUI();
        }
        
        else {
                 
            if(lscompany=='Company') {
                 var params = {
                 colendingprogram_gid: colendingprogram_gid,
                 application_gid:application_gid,                               
                 applicant_type: lscompany
                 }
             }
             else if(lsindividual=='Individual') {                
                 var params = {
                 colendingprogram_gid: colendingprogram_gid,
                 application_gid:application_gid,                                
                 applicant_type: lsindividual
                 }
             }
             else if(lsgroup=='Group') {            
                 var params = {
                 colendingprogram_gid: colendingprogram_gid,
                 application_gid:application_gid,                                
                 applicant_type: lsgroup
                 }
             }            
             else{
                 var params = {
                     colendingprogram_gid: colendingprogram_gid,
                     application_gid:application_gid,
                     applicant_type: "Company"               
                 }
             }
 
             var url = 'api/MstAppCreditUnderWriting/GetColendingBasicView';
 
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();               
                 $scope.txtcolendar_name = resp.data.colendar_name;               
                 if (resp.data.scorecard_submit == "0" || resp.data.scorecard_submit == "") {
                     lockUI();
                     $scope.showsubmitscoreevent = true;
                     var url = 'api/MstAppCreditUnderWriting/GetColendingScorecarddtl';
                     SocketService.getparams(url, params).then(function (resp) {
                         unlockUI();
                         $scope.GroupTitle_List_Colending = resp.data.Colending_GroupTitle_dtl;
                         $scope.GroupQuestion_list_Colending = resp.data.MdlTrnColendingGroupTitleQuestion;
                         $scope.grouplistdtl = resp.data.Colending_listarray;
 
                         angular.forEach($scope.GroupQuestion_list_Colending, function (value, key) {
                             if (value.colendingquestionrule_gid != "") {
                                 var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                 if (getDropdownListArray != null) {
                                     value.DropdownListArraydtl = getDropdownListArray;
                                 }
                             }
                         });
 
                         angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                             if (value.grouptitle_gid != "") {
                                 var getGroupQuestionListArray = $scope.GroupQuestion_list_Colending.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                 if (getGroupQuestionListArray != null) {
                                     value.GroupQuestion_list_Colending = getGroupQuestionListArray;
                                 }
                             }
                         });
                     });
                 }
                 else {
                     $scope.showsubmitscoreevent = false;
                     lockUI();
                     var url = 'api/MstAppCreditUnderWriting/GetColendingScorecardViewdtl';
                     SocketService.getparams(url, params).then(function (resp) {
                         unlockUI();
                         $scope.GroupTitle_List_Colending = resp.data.Colending_GroupTitle_dtl;
                         $scope.GroupQuestion_list_Colending = resp.data.MdlTrnColendingGroupTitleQuestion;
 
                         $scope.grouplistdtl = resp.data.Colending_listarray;
 
                         angular.forEach($scope.GroupQuestion_list_Colending, function (value, key) {
                             if (value.colendingquestionrule_gid != "") {
                                 var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.colendingquestionrule_gid === value.colendingquestionrule_gid });
                                 if (getDropdownListArray != null) {
                                     value.DropdownListArraydtl = getDropdownListArray;
                                     value.cbofield_type = value.actualvalue_gid;
                                 }
 
                                 value.field_number = value.actual_value;
                             }
                         });
 
                         angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                             if (value.grouptitle_gid != "") {
                                 var getGroupQuestionListArray = $scope.GroupQuestion_list_Colending.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                 if (getGroupQuestionListArray != null) {
                                     value.GroupQuestion_list_Colending = getGroupQuestionListArray;
                                 }
                             }
                         });
                     });
                 } 
                 unlockUI();
             });
         }

        }

        // function defaultamountwordschange(input) {
        //     var str1 = input.replace(/,/g, '');
        //     var str = Math.round(str1);
        //     var output = Number(str).toLocaleString('en-IN');
        //     var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
        //     return lswords;
        // }
        
        $scope.Back = function () {
            if(lspagetype=='Credit'){

                if (lscompany=='Company') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditColendingDetailsAdd');
                }
                else if (lsindividual=='Individual') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditIndividualColendingDtlAdd');
                } 
                else if (lsgroup=='Group') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditGroupColendingDtlAdd');
                }           
                else {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&lspage=' + lspage )
                } 
            }
            else if(lspagetype=='CC'|| lspagetype=='CAD_Pending' || lspagetype=='PendingCADReview'){

                if (lscompany=='Company') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCcCommitteeInstitutionView');
                }
                else if (lsindividual=='Individual') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCcCommitteeIndividualView');
                } 
                else if (lsgroup=='Group') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCcCommitteeGroupView');
                }           
                else {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&lspage=' + lspage )
                } 
            }
            else if(lspagetype=='CAD_Accepted'){

                if (lscompany=='Company') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditColendingDetailsAdd');
                }
                else if (lsindividual=='Individual') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditIndividualColendingDtlAdd');
                } 
                else if (lsgroup=='Group') {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage + '&lspagetype=' + lspagetype )
                    //    $state.go('app.MstCreditGroupColendingDtlAdd');
                }           
                else {
                    $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&lspage=' + lspage )
                } 
            }
            else {
                $location.url('app/'+lspagename+'?application_gid=' + application_gid + '&lspage=' + lspage )
            }
                         
        }
    
        $scope.changefieldtype = function (index, cbofield_type, ColendingDropdownListArray,questionindex) {
            var sum = 0;
            angular.forEach($scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending, function (value, key) {
                if (value.colendingquestionrule_gid == cbofield_type.colendingquestionrule_gid) {
                    if (cbofield_type.Score == 'Rejected') {
                        value.ColendingDropdownListArray = cbofield_type;
                        value.final_score = "0";
                        if (value.answer_type == "List")
                            value.Score = "Rejected";
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = "0";
                    }
                    else {
                        value.final_score = cbofield_type.Score;
                        value.ColendingDropdownListArray = [];
                        value.ColendingDropdownListArray.push(cbofield_type);
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = cbofield_type.Score;
                        if (value.answer_type == "List")
                            value.Score = cbofield_type.Score;
                    }
                   
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_List_Colending[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending.filter(function (el) { return el.colendingquestionrule_gid === cbofield_type.colendingquestionrule_gid });
            var lscolendingquestionrule_gid = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending[questionindex].colendingquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                var params = {
                    colendingquestionrule_gid: lscolendingquestionrule_gid,
                    GroupTitle_List_Colending: $scope.GroupTitle_List_Colending,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid, 
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_List_Colending, function (value, key) { 
                            angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.colendingquestionrule_gid === value1.colendingquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            }); 
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                             sum = 0;
                            angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
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
            var colendingquestionrule_gid = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending[questionruleindex].colendingquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending, function (value, key) {

                if (value.colendingquestionrule_gid == colendingquestionrule_gid) {
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
            $scope.GroupTitle_List_Colending[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending.filter(function (el) { return el.colendingquestionrule_gid === colendingquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        colendingquestionrule_gid: colendingquestionrule_gid,
                        GroupTitle_List_Colending: $scope.GroupTitle_List_Colending,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetColendingQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.colendingquestionrule_gid === value1.colendingquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
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

        $scope.changefieldtypeEdit = function (index, cbofield_type, ColendingDropdownListArray, questionindex) {
            $scope.btnupdateshow = true;
            var sum = 0;
            cbofield_type = ColendingDropdownListArray.filter(function (el) { return el.colendingquestionlistoption_gid === cbofield_type });
            angular.forEach($scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending, function (value, key) {
                if (value.colendingquestionrule_gid == cbofield_type[0].colendingquestionrule_gid) {
                    if (cbofield_type[0].Score == 'Rejected') {
                        value.ColendingDropdownListArray = cbofield_type;
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
                        value.ColendingDropdownListArray = [];
                        value.ColendingDropdownListArray.push(cbofield_type[0]);
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
                        value.ColendingDropdownListArray = [];
                        var getlist = value.DropdownListArraydtl.filter(function (el) { return el.colendingquestionlistoption_gid === value.cbofield_type });
                        value.ColendingDropdownListArray = getlist;
                    }
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_List_Colending[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending.filter(function (el) { return el.colendingquestionrule_gid === cbofield_type[0].colendingquestionrule_gid });
            var lscolendingquestionrule_gid = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending[questionindex].colendingquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                var params = {
                    colendingquestionrule_gid: lscolendingquestionrule_gid,
                    GroupTitle_List_Colending: $scope.GroupTitle_List_Colending,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid,
                }
                var url = 'api/MstAppCreditUnderWriting/GetColendingQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                            angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.colendingquestionrule_gid === value1.colendingquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            });
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                            sum = 0;
                            angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
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
            var colendingquestionrule_gid = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending[questionruleindex].colendingquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending, function (value, key) {

                if (value.colendingquestionrule_gid == colendingquestionrule_gid) {
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
            $scope.GroupTitle_List_Colending[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_List_Colending[index].GroupQuestion_list_Colending.filter(function (el) { return el.colendingquestionrule_gid === colendingquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        colendingquestionrule_gid: colendingquestionrule_gid,
                        GroupTitle_List_Colending: $scope.GroupTitle_List_Colending,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstAppCreditUnderWriting/GetColendingQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.colendingquestionrule_gid === value1.colendingquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_List_Colending, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list_Colending, function (value1, key1) {
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

    }
})();