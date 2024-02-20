(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstEditWaiverController', MstEditWaiverController);

        MstEditWaiverController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstEditWaiverController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstEditWaiverController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.rmpostccwaiver_gid = $location.search().rmpostccwaiver_gid;
        var rmpostccwaiver_gid = $scope.rmpostccwaiver_gid;
        
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.sanctionview = true;
        $scope.lanwaiver = true;
        activate();

        function activate() {
            $scope.cbosanctionwaivercategory = [];
            $scope.cbolanwaivercategory = [];
            $scope.cboWaiverGroup = [];
            $scope.cbolan = [];
            
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

            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid
            }

            var url = 'api/MstRMPostCCWaiver/EditRMPostCCWaiver';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cbowaivercategory = resp.data.waiver_category;

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

                $scope.cbosanctionref_no = resp.data.sanction_refno;
               
                $scope.cbourn = resp.data.urn;
                $scope.txtcustomer_info = resp.data.customer_info;
                $scope.txtwaiver_title = resp.data.waiver_title;
                $scope.txtwaiver_description = resp.data.waiver_description;
                $scope.txtwaiver_amount = resp.data.waiver_amount;
                $scope.approval_type = resp.data.approval_type;
                $scope.txtapproval_remarks = resp.data.approval_remarks;

                $scope.sanctionwaivergen_list = resp.data.sanctionwaivergen_list;
                $scope.sanctionwaiver_list = resp.data.sanctionwaiver_list;
                if (resp.data.sanctionwaiver_list != null) {
                    var count = resp.data.sanctionwaiver_list.length;
                    for (var i = 0; i < count; i++) {                       
                        var indexs = $scope.sanctionwaivergen_list.map(function (x) { return x.sanctionwaiver_gid; }).indexOf(resp.data.sanctionwaiver_list[i].sanctionwaiver_gid);
                        $scope.cbosanctionwaivercategory.push($scope.sanctionwaivergen_list[indexs]);
                        $scope.$parent.cbosanctionwaivercategory = $scope.cbosanctionwaivercategory;
                    }
                }

                $scope.lanwaivergen_list = resp.data.lanwaivergen_list;
                $scope.lanwaiver_list = resp.data.lanwaiver_list;
                if (resp.data.lanwaiver_list != null) {
                    var count = resp.data.lanwaiver_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.lanwaivergen_list.map(function (x) { return x.lanwaiver_gid; }).indexOf(resp.data.lanwaiver_list[i].lanwaiver_gid);
                        $scope.cbolanwaivercategory.push($scope.lanwaivergen_list[indexs]);
                        $scope.$parent.cbolanwaivercategory = $scope.cbolanwaivercategory;
                    }
                }

                $scope.waivergroupgen_list = resp.data.waivergroupgen_list;
                $scope.waivergroup_list = resp.data.waivergroup_list;
                if (resp.data.waivergroup_list != null) {
                    var count = resp.data.waivergroup_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.waivergroupgen_list.map(function (x) { return x.groupwaiver_gid; }).indexOf(resp.data.waivergroup_list[i].groupwaiver_gid);
                        $scope.cboWaiverGroup.push($scope.waivergroupgen_list[indexs]);
                        $scope.$parent.cboWaiverGroup = $scope.cboWaiverGroup;
                    }
                }

                $scope.langen_list = resp.data.langen_list;
                $scope.lan_list = resp.data.lan_list;
                if (resp.data.lan_list != null) {
                    var count = resp.data.lan_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.langen_list.map(function (x) { return x; }).indexOf(resp.data.lan_list[i]);
                        $scope.cbolan.push($scope.langen_list[indexs]);
                        $scope.$parent.cbolan = $scope.cbolan;
                    }
                }
                
            });

            var url = 'api/MstRMPostCCWaiver/WaiverDocList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.uploadwaiverdoc_list = resp.data.uploadwaiverdoc_list;
            });

            var url = 'api/MstRMPostCCWaiver/ApprovalMemberList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.waiverapprovalmember_list = resp.data.waiverapprovalmember_list;
            });

            
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
        var url = 'api/MstRMPostCCWaiver/GetCustomerAndLANFromURN';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
        unlockUI();
        $scope.txtcustomer_info = resp.data.customer_info;  
        $scope.lan_list = resp.data.lan_list;  
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
            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid
            }
            var url = 'api/MstRMPostCCWaiver/WaiverDocTempList';
            SocketService.getparams(url,params).then(function (resp) {
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

        $scope.approvalmember_add = function () {

            if (($scope.cboapproval_member == undefined) || ($scope.cboapproval_member == '') || ($scope.cboapproval_member == null)) {
                Notify.alert('Kindly Select the Approval Member..!','warning');
            }
            else {
                var params = {
                    approvalmember_gid: $scope.cboapproval_member.employee_gid,
                    approvalmember_name: $scope.cboapproval_member.employee_name,
                    rmpostccwaiver_gid: $scope.rmpostccwaiver_gid      
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
            var params = {
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid
            }
            var url = 'api/MstRMPostCCWaiver/ApprovalMemberTempList';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.waiverapprovalmember_list = resp.data.waiverapprovalmember_list;
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

        $scope.Waiver_Update = function () {

            var params = {
                application_gid: $scope.application_gid,
                waiver_category : $scope.cbowaivercategory,
                sanction_refno : $scope.cbosanctionref_no,
                lan_list: $scope.cbolan,
                urn: $scope.cbourn,
                sanctionwaiver_list: $scope.cbosanctionwaivercategory,
                lanwaiver_list: $scope.cbolanwaivercategory,
                customer_info: $scope.txtcustomer_info,
                waiver_title: $scope.txtwaiver_title,
                waiver_description: $scope.txtwaiver_description,
                waiver_amount: $scope.txtwaiver_amount,
                waivergroup_list: $scope.cboWaiverGroup,
                approval_type: $scope.approval_type,
                approval_remarks: $scope.txtapproval_remarks,
                rmpostccwaiver_gid: $scope.rmpostccwaiver_gid
            }
            var url = 'api/MstRMPostCCWaiver/UpdateRMPostCCWaiver';
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
