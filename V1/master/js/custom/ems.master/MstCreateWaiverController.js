(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreateWaiverController', MstCreateWaiverController);

    MstCreateWaiverController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCreateWaiverController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreateWaiverController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.sanctionview = true;
        $scope.lanwaiver = true;
        activate();

        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                //$scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbusinessapproved_date = resp.data.headapproval_date;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.txtregion = resp.data.region;
            });

            var params = {
                servicerequest_gid: 'SERQ202110263134'
            }
            var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
            });
        }

        $scope.txtWaiveramount_change = function () {
            var input = document.getElementById('words_Waiver').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_Waiver = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtwords_Waiver = "";
            }
            else {
                $scope.txtwords_Waiver = output;
                document.getElementById('wordsWaiver').innerHTML = lswords_Waiver;
            }
        }

        

        $scope.change_waivercategory = function (cbowaivercategory) {
            if ($scope.cbowaivercategory == 'Sanction Waiver') {
                $scope.sanctionviewdrop = true;
                $scope.sanctionview = false;
                $scope.lanwaiverdrop = false;
                $scope.lanwaiver = false;
            }
            else if ($scope.cbowaivercategory == 'LAN Waiver') {
                $scope.lanwaiverdrop = true;
                $scope.lanwaiver = false;
                $scope.sanctionviewdrop = false;
                $scope.sanctionview = false;
            }            
            else {
                $scope.sanctionview = true;
                $scope.lanwaiver = true;
                $scope.sanctionviewdrop = false;
                $scope.lanwaiverdrop = false;
            }
        }

        $scope.document_upload = function (val, val1, name) {
            lockUI();
            if (($scope.txtuploaddocument == null) || ($scope.txtuploaddocument == '') || ($scope.txtuploaddocument == undefined)) {
                $("#fileIndividuaDocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
                unlockUI();
            }
            else {
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                }

                frm.append('document_title', $scope.txtuploaddocument);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    //var url = 'api/MstApplicationAdd/IndividualDocumentUpload';
                    //SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    //    $("#fileIndividuaDocument").val('');
                    //    $scope.cboIndividualDocument = '';
                    //    unlockUI();
                    //    if (resp.data.status == true) {
                    //        Notify.alert(resp.data.message, {
                    //            status: 'success',
                    //            pos: 'top-center',
                    //            timeout: 3000
                    //        });
                    //    }
                    //    else {
                    //        Notify.alert(resp.data.message, {
                    //            status: 'warning',
                    //            pos: 'top-center',
                    //            timeout: 3000
                    //        });
                    //    }

                    //    var url = 'api/MstApplicationAdd/GetIndividualDocList';
                    //    SocketService.get(url).then(function (resp) {
                    //        $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
                    //    });

                    //    unlockUI();
                    //});
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        $scope.ApprovalMembercancel = function () {
            //var params = {
            //    tmpapprovalmember_gid: tmpapprovalmember_gid,
            //    servicerequest_gid: servicerequest_gid,
            //}
            //var url = 'api/OsdTrnMyTicket/TmpApprovalMembersDelete';
            //SocketService.post(url, params).then(function (resp) {
            //    $scope.approvalmember = resp.data.approvalmember;

            //    var param = {
            //        servicerequest_gid: servicerequest_gid
            //    }
            //    var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
            //    SocketService.getparams(url, param).then(function (resp) {
            //        $scope.employee_list = resp.data.employeelist;
            //    });


            //});



        }

        $scope.Waiver_Submit = function () {
                //var params = {
                //   waivercategory : $scope.cbowaivercategory,
                //   sanctionref_no : $scope.cbosanctionref_no,
                //   lan: $scope.cbolan,
                //   urn: $scope.cbourn,
                //   sanctionwaivercategory: $scope.cbosanctionwaivercategory,
                //   lanwaivercategory: $scope.cbolanwaivercategory,
                //   customer_info: $scope.txtcustomer_info,
                //   waiver_title: $scope.txtwaiver_title,
                //   waiver_remarks: $scope.txtwaiver_remarks,
                //   words_Waiver: $scope.txtwords_Waiver,
                //   WaiverGroup: $scope.cboWaiverGroup,
                //   approval_type: $scope.approval_type,
                //   approval_member: $scope.cboapproval_member

                //}
                //var url = 'api/MstApplicationEdit/SubmitIndividualDtlAdd';
                //lockUI();
                //SocketService.post(url, params).then(function (resp) {
                //    unlockUI();
                //    if (resp.data.status == true) {
                //        Notify.alert(resp.data.message, {
                //            status: 'success',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
                //    $state.go('app.MstRMInitiateWaiverSummary');
                //    }
                //    else {
                //        Notify.alert(resp.data.message, {
                //            status: 'warning',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
            //         $state.go('app.MstRMInitiateWaiverSummary');
                //    }

            //});

            $state.go('app.MstRMInitiateWaiverSummary');
        }

        $scope.Back = function () {
            $state.go('app.MstRMInitiateWaiverSummary');
        }

    }
})();
