(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAddWaiverController', MstAddWaiverController);

    MstAddWaiverController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstAddWaiverController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAddWaiverController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.sanctionview = true;
        $scope.lanwaiver = true;
        activate();

        function activate() {

            var url = 'api/MstRMPostCCWaiver/GetWaiverTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstRMPostCCWaiver/GetApplicationWaiverMaster';
            SocketService.get(url).then(function (resp) {
               $scope.sanctionwaiver_list = resp.data.sanctionwaiver_list;
               $scope.lanwaiver_list = resp.data.lanwaiver_list;
               $scope.waivergroup_list = resp.data.waivergroup_list;
            });

            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
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

        // function inWords(num) {
        //     var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        //     var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        //     var s = num.toString();
        //     s = s.replace(/[\, ]/g, '');
        //     if (s != parseFloat(s)) return '';
        //     if ((num = num.toString()).length > 9) return 'Overflow';
        //     var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        //     if (!n) return; var str = '';
        //     str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
        //     str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
        //     str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
        //     str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

        //     str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
        //     return str;
        // }

        $scope.change_waivercategory = function (cbowaivercategory) {
            if ($scope.cbowaivercategory == 'Sanction Waiver') {
                $scope.sanctionviewdrop = true;
                $scope.sanctionview = false;
                $scope.lanwaiverdrop = false;
                $scope.lanwaiver = false;
            }
            else if ($scope.cbowaivercategory == 'LAN Waiver') {
                $scope.lanwaiverdrop = false;
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

            var params =
            {
                application_gid: $scope.application_gid
            }
        var url = 'api/MstRMPostCCWaiver/GetApplicationWaiverDetail';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
        unlockUI();
            $scope.sanctionrefno_list = resp.data.sanctionrefno_list;
            $scope.urn_list = resp.data.urn_list;
        });

        }

        $scope.change_urn = function (cbourn) { 
            var params =
            {
                urn: cbourn
            }
            lockUI();
        var url = 'api/MstRMPostCCWaiver/GetCustomerAndLANFromURN';       
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.txtcustomer_info = resp.data.customer_info;  
            $scope.lan_list = resp.data.lan_list;   
            if($scope.cbowaivercategory == 'LAN Waiver') {
                $scope.lanwaiverdrop = true;
            }              
        });        
        }
    

        $scope.waiverdocument_upload = function (val, val1, name) {
            lockUI();
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#fileWaiverDocument").val('');
                Notify.alert('Kindly Enter the Document Title..!', 'warning');
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
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }

                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstRMPostCCWaiver/WaiverDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#fileWaiverDocument").val('');
                        $scope.cboIndividualDocument = '';
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            waiverdocument_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        function waiverdocument_list() {
            var url = 'api/MstRMPostCCWaiver/GetWaiverDocList';
            SocketService.get(url).then(function (resp) {
                $scope.uploadwaiverdoc_list = resp.data.uploadwaiverdoc_list;
            });
        }

        $scope.waiverdocument_delete = function (rmpostccwaiver2document_gid) {
            var params = {
                rmpostccwaiver2document_gid: rmpostccwaiver2document_gid
            }
            lockUI();
            var url = 'api/MstRMPostCCWaiver/WaiverDocDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    waiverdocument_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.uploadwaiverdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadwaiverdoc_list[i].document_path, $scope.uploadwaiverdoc_list[i].document_name);
            }
        }

        $scope.approvalmember_add = function () {

            if (($scope.cboapproval_member == undefined) || ($scope.cboapproval_member == '') || ($scope.cboapproval_member == null)) {
                Notify.alert('Kindly Select the Approval Member..!','warning');
            }
            else {
                var params = {
                    approval_type: $scope.approval_type,
                    approvalmember_gid: $scope.cboapproval_member.employee_gid,
                    approvalmember_name: $scope.cboapproval_member.employee_name,     
                }
                var url = 'api/MstRMPostCCWaiver/PostApprovalMember';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        approvalmember_list();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }   
                    $scope.cboapproval_member = '';        
                });

            }

        }

        function approvalmember_list() {
            var url = 'api/MstRMPostCCWaiver/GetApprovalMemberList';
            SocketService.get(url).then(function (resp) {
                $scope.waiverapprovalmember_list = resp.data.waiverapprovalmember_list;
                if($scope.waiverapprovalmember_list.length == 0) {
                    $scope.disableSequence = false;
                    $scope.disableParallel = false; 
                } else {
                    if($scope.approval_type=='Sequence') {
                        $scope.disableParallel = true;                       
                    }
                    else {
                        $scope.disableSequence = true;
                    }
                }
            });
        }

        $scope.approvalmember_delete = function (rmpostccwaiver2approvalmember_gid) {
            var params =
                {
                    rmpostccwaiver2approvalmember_gid: rmpostccwaiver2approvalmember_gid
                }
            var url = 'api/MstRMPostCCWaiver/ApprovalMemberDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    approvalmember_list();
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

        $scope.Waiver_Submit = function () {
                var params = {
                  application_gid: $scope.application_gid,
                  waiver_category : $scope.cbowaivercategory,
                  sanction_refno : $scope.cbosanctionref_no,
                  lan_list: $scope.cbolan,
                  urn: $scope.cbourn,
                  vernacularlanguage_list: $scope.cboVernacularLanguage,
                  sanctionwaiver_list: $scope.cbosanctionwaivercategory,
                  lanwaiver_list: $scope.cbolanwaivercategory,
                  customer_info: $scope.txtcustomer_info,
                  waiver_title: $scope.txtwaiver_title,
                  waiver_description: $scope.txtwaiver_description,
                  waiver_amount: $scope.txtwaiver_amount,
                  waivergroup_list: $scope.cboWaiverGroup,
                  approval_type: $scope.approval_type,
                  approval_remarks: $scope.txtapproval_remarks

                }
                var url = 'api/MstRMPostCCWaiver/SubmitRMPostCCWaiver';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $location.url('app/MstRMInitiateWaiverSummary?application_gid=' + application_gid);
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

        $scope.Back = function () {
            $location.url('app/MstRMInitiateWaiverSummary?application_gid=' + application_gid);
        }

    }
})();
