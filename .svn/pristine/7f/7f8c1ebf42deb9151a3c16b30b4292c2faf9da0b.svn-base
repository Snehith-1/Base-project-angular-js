(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupDtlEditController', MstCADGroupDtlEditController);

    MstCADGroupDtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCADGroupDtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupDtlEditController';

        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
        
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MstCADApplication/GetGroupTempClear';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
   
            var url = 'api/MstApplication360/GetInternalRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.internalrating_list = resp.data.internalrating_list;
            });

            var param = {
                group_gid: $scope.group_gid
            };          
           
            var url = 'api/MstCADApplication/EditGroup';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtdate_of_formation = resp.data.date_of_formation;
                $scope.cboGroupType = resp.data.group_type;
                $scope.txtgroupmember_count = resp.data.groupmember_count;
                $scope.rdbgroupurn_status = resp.data.groupurn_status;
                $scope.txtgroup_urn = resp.data.group_urn;
                $scope.group_status = resp.data.group_status;
                $scope.cbointernalrating = resp.data.internalrating_gid;
                $scope.txtmale_count = resp.data.male_count;
                $scope.txtfemale_count = resp.data.female_count;
                if (resp.data.group_status == "Incomplete") {
                    $scope.GroupSubmit = true;
                    $scope.GroupUpdate = false;
                }
                else {
                    $scope.GroupSubmit = false;
                    $scope.GroupUpdate = true;
                }

                var url = 'api/MstCADApplication/GetEditAddGroupEquipmentHoldingList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
                });

                var url = 'api/MstCADApplication/GetEditAddGroupLivestockList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
                });

                $scope.onselectedurn_yes = function () {
                    if ($scope.rdbgroupurn_status == 'Yes') {
                        $scope.URN_yes = true;
                    }
                    else {
                        $scope.URN_yes = false;
                    }
                }

                if (resp.data.groupurn_status == 'Yes') {
                    $scope.URN_yes = true;
                }
                else {
                    $scope.URN_yes = false;
                }

               
            });

            var url = 'api/MstCADApplication/GroupDocumentList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.groupdocument_list = resp.data.groupdocument_list;
            });

            var url = 'api/MstCADApplication/GroupBankList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mstbank_list = resp.data.mstbank_list;
            });

            var url = 'api/MstCADApplication/GroupAddressList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mstaddress_list = resp.data.mstaddress_list;
            });

            var url = 'api/MstApplication360/GroupDocumentList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.groupdoc_list = resp.data.groupdoc_list;
            });

        }

        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/MstApplicationAdd/FutureDateCheck';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }

        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/CADKyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbank_branch = resp.data.result.branch;

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbank_branch = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.BankAccValidation = function () {

            var params = {
                ifsc: $scope.txtifsc_code,
                accountNumber: $scope.txtbank_accountno
            }

            var url = 'api/CADKyc/BankAccVerification';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                    $scope.bankaccvalidation = true;
                    $scope.txtaccountholder_name = resp.data.result.accountName;

                } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                    $scope.bankaccvalidation = false;
                    Notify.alert('Bank Account is not verified..!', 'warning');
                    $scope.txtaccountholder_name = '';
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });

        }

        function groupaddresstemp_list() {
            var param = {
                group_gid: $scope.group_gid
            };
            var url = 'api/MstCADApplication/GroupAddressTmpList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mstaddress_list = resp.data.mstaddress_list;
            });
        }

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
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
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        address_typegid: $scope.cboAddressType.address_gid,
                        address_type: $scope.cboAddressType.address_type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtlandmark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        group_gid: $location.search().group_gid,
                       
                    }
                    var url = 'api/MstCADApplication/PostGroupAddressDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            groupaddresstemp_list();
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

        $scope.address_edit = function (group2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype_edit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    group2address_gid: group2address_gid
                }
                var url = 'api/MstCADApplication/EditGroupAddressDetail';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.cboAddressType = resp.data.address_typegid;

                    $scope.rdbprimary_status = resp.data.primary_status;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.txtcountry = "India";
                $scope.address_update = function () {
                    var address_Type = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboAddressType,
                        address_type: address_Type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        group2address_gid: group2address_gid,
                        group_gid: $location.search().group_gid,
                        statusupdated_by: 'Credit',
                    }
                    var url = 'api/MstCADApplication/UpdateGroupAddressDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            groupaddresstemp_list();
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

        $scope.address_delete = function (group2address_gid) {
            var params =
                {
                    group2address_gid: group2address_gid
                }
            var url = 'api/MstApplicationAdd/DeleteGroupAddressDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    groupaddresstemp_list();
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

        function groupbanktemp_list() {
            var param = {
                group_gid: $scope.group_gid
            };
            var url = 'api/MstCADApplication/GroupBankTmpList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mstbank_list = resp.data.mstbank_list;

            });
        }

        $scope.bank_add = function () {

            if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbank_branch == '') || ($scope.txtbank_branch == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined) || ($scope.txtbank_accountno == '') || ($scope.txtbank_accountno == undefined) || ($scope.txtaccountholder_name == '') || ($scope.txtaccountholder_name == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;
                var params = {
                    ifsc_code: $scope.txtifsc_code,
                    bank_accountno: $scope.txtbank_accountno,
                    accountholder_name: $scope.txtaccountholder_name,
                    bank_name: $scope.txtbank_name,
                    bank_branch: $scope.txtbank_branch,
                    group_gid: $scope.group_gid,
                }
                var url = 'api/MstCADApplication/PostGroupBankDetail';
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
                    groupbanktemp_list();
                    $scope.txtbank_name = '';
                    $scope.txtbank_branch = '';
                    $scope.txtifsc_code = '';
                    $scope.txtbank_accountno = '';
                    $scope.txtaccountholder_name = '';
                    $scope.ifscvalidation = false;
                    $scope.bankaccvalidation = false;

                });
            }
        }

        $scope.bank_edit = function (group2bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbankdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    group2bank_gid: group2bank_gid
                }
                var url = 'api/MstCADApplication/EditGroupBankDetail';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtifsc_code = resp.data.ifsc_code;
                    $scope.txtbank_accountno = resp.data.bank_accountno;
                    $scope.txtaccountholder_name = resp.data.accountholder_name;
                    $scope.txtbank_name = resp.data.bank_name;
                    $scope.txtbank_branch = resp.data.bank_branch;
                    $scope.group_gid = resp.data.group_gid;
                    $scope.group2bank_gid = resp.data.group2bank_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_bank = function () {
                   
                    var params = {                        
                        ifsc_code: $scope.txtifsc_code,
                        bank_accountno: $scope.txtbank_accountno,
                        accountholder_name: $scope.txtaccountholder_name,
                        bank_name: $scope.txtbank_name,
                        bank_branch: $scope.txtbank_branch,
                        group_gid: $location.search().group_gid,
                        group2bank_gid: $scope.group2bank_gid,
                        statusupdated_by: 'Credit',
                    }
                    var url = 'api/MstCADApplication/UpdateGroupBankDetail';
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
                        groupbanktemp_list();
                    });

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.bank_delete = function (group2bank_gid) {
            var params =
                {
                    group2bank_gid: group2bank_gid
                }
            console.log(params)
            var url = 'api/MstCADApplication/DeleteGroupBankDetail';
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

                groupbanktemp_list();
            });

        }


        function groupdocumenttemp_list() {
            var param = {
                group_gid: $scope.group_gid
            };
            var url = 'api/MstCADApplication/GroupDocumentTmpList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.groupdocument_list = resp.data.groupdocument_list;
            });
        }

        $scope.groupdocument_upload = function (val, val1, name) {
            lockUI();
            if (($scope.cbogroupdocumentname == '') || ($scope.cbogroupdocumentname == undefined)) {
                $("#fileGroupDocument").val('');
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

                frm.append('document_title', $scope.cbogroupdocumentname.groupdocument_name);
                frm.append('groupdocument_gid', $scope.cbogroupdocumentname.groupdocument_gid);

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstCADApplication/GroupDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#fileGroupDocument").val('');
                        $scope.cbogroupdocumentname = '';
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

                        groupdocumenttemp_list();

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

        $scope.groupdocument_delete = function (group2document_gid) {

            var params = {
                group2document_gid: group2document_gid
            }
            lockUI();
            var url = 'api/MstCADApplication/GroupDocumentDelete';
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                groupdocumenttemp_list();
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
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
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

           $scope.group_update = function () {
            if (($scope.URN_yes == true) && (($scope.txtgroup_urn == '') || ($scope.txtgroup_urn == undefined))) {
                Notify.alert('Enter URN value', 'warning');
            }
            else {
                var dateParts, dateObject;
                try {
                    dateParts = $scope.txtdate_of_formation.split("-");
                    dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
                }
                catch (err) {
                    dateObject = $scope.txtdate_of_formation;
                }
                var internalratingname = $('#internalrating_name :selected').text();
                var params = {
                    group_name: $scope.txtgroup_name,
                    dateofformation: dateObject,
                    group_type: $scope.cboGroupType,
                    groupmember_count: $scope.txtgroupmember_count,
                    groupurn_status: $scope.rdbgroupurn_status,
                    group_urn: $scope.txtgroup_urn,
                    group_gid: $scope.group_gid,
                    statusupdated_by: 'Credit',
                    application_gid: $scope.application_gid,
                    internalrating_gid: $scope.cbointernalrating,
                    internalrating_name: internalratingname,
                    male_count: $scope.txtmale_count,
                    female_count: $scope.txtfemale_count
                }

                var url = 'api/MstCADApplication/UpdateGroupDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lspage == "StartCreditUnderwriting") {
                            $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
                        }
                        else if (lspage == "CADApplicationEdit") {
                            $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                        }
                        else if (lspage == "CADAcceptanceCustomers") {
                            $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                        }
                        else if (lspage == "PendingCADReview") {
                            $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                        }
                        else {

                        }
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

           $scope.groupdtl_Back = function () {
               if (lspage == "StartCreditUnderwriting") {
                   $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
               }
               else if (lspage == "CADApplicationEdit") {
                   $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
               }
               else if (lspage == "CADAcceptanceCustomers") {
                   $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
               }
               else if (lspage == "PendingCADReview") {
                   $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
               }
               else {

               }
           }
           $scope.addequipment_holding = function () {
               var modalInstance = $modal.open({
                   templateUrl: '/Equipmentholding.html',
                   controller: ModalInstanceCtrl,
                   backdrop: 'static',
                   keyboard: false,
                   size: 'lg'
               });
               ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
               function ModalInstanceCtrl($scope, $modalInstance) {

                   $scope.insurancestatus_yes = function () {
                       $scope.insurancestatusyesshow = true;
                   }
                   $scope.insurancestatus_no = function () {
                       $scope.insurancestatusyesshow = false;
                   }

                   var url = 'api/MstApplication360/GetEquipmentHoldingList';
                   SocketService.get(url).then(function (resp) {
                       $scope.equipment_list = resp.data.equipment_list;
                   });

                   $scope.equipment_submit = function () {
                       var lsequipment_gid = '';
                       var lsequipment_name = '';

                       if ($scope.cboequipmentname != undefined || $scope.cboequipmentname != null) {
                           lsequipment_gid = $scope.cboequipmentname.equipment_gid;
                           lsequipment_name = $scope.cboequipmentname.equipment_name;
                       }

                       var params = {
                           equipment_gid: lsequipment_gid,
                           equipment_name: lsequipment_name,
                           availablerenthire: $scope.rdbavailablerent,
                           quantity: $scope.txtquantity,
                           description: $scope.txtdescription,
                           insurance_status: $scope.rdbinsurancestatus,
                           insurance_details: $scope.txtinsurancedetail
                       }
                       var url = 'api/MstCADApplication/PostGroupEquipmentHolding';
                       lockUI();
                       SocketService.post(url, params).then(function (resp) {
                           unlockUI();
                           if (resp.data.status == true) {

                               Notify.alert(resp.data.message, {
                                   status: 'success',
                                   pos: 'top-center',
                                   timeout: 3000
                               });
                               equipmentholding_list();
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

                   $scope.ok = function () {
                       $modalInstance.close('closed');
                   };
               }
           }

           $scope.deleteequipment_holding = function (group2equipment_gid) {
               var params = {
                   group2equipment_gid: group2equipment_gid
               }
               var url = 'api/MstCADApplication/DeleteGroupEquipmentHolding';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {
                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       equipmentholding_list();
                   }
                   else {
                       Notify.alert(resp.data.message, {
                           status: 'warning',
                           pos: 'top-center',
                           timeout: 3000
                       });
                   }
                   equipmentholding_list();
               });

           }

           $scope.equipment_View = function (group2equipment_gid) {
               var modalInstance = $modal.open({
                   templateUrl: '/EquipmentholdingView.html',
                   controller: ModalInstanceCtrl,
                   backdrop: 'static',
                   keyboard: false,
                   size: 'lg'
               });
               ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
               function ModalInstanceCtrl($scope, $modalInstance) {
                   var params = {
                       group2equipment_gid: group2equipment_gid
                   }
                   var url = 'api/MstCADApplication/GetGroupEquipmentHoldingView';
                   lockUI();
                   SocketService.getparams(url, params).then(function (resp) {
                       unlockUI();
                       $scope.lblquantity = resp.data.quantity;
                       $scope.lbldescription = resp.data.description;
                       $scope.lblinsurancedetails = resp.data.insurance_details;
                   });

                   $scope.ok = function () {
                       $modalInstance.close('closed');
                   };


               }

           }

           function equipmentholding_list() {
               var params = {
                   group_gid: $scope.group_gid
               }
               var url = 'api/MstCADApplication/GetEditGroupEquipmentHoldingList';
               SocketService.getparams(url, params).then(function (resp) {
                   $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;

               });
           }

           $scope.addlivestock_holding = function () {
               var modalInstance = $modal.open({
                   templateUrl: '/Livestockholding.html',
                   controller: ModalInstanceCtrl,
                   backdrop: 'static',
                   keyboard: false,
                   size: 'lg'
               });
               ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
               function ModalInstanceCtrl($scope, $modalInstance) {

                   $scope.insurancestatus_yes = function () {
                       $scope.insurancestatusyesshow = true;
                   }
                   $scope.insurancestatus_no = function () {
                       $scope.insurancestatusyesshow = false;
                   }

                   var url = 'api/MstApplication360/GetLivestockList';
                   SocketService.get(url).then(function (resp) {
                       $scope.livestock_list = resp.data.livestock_list;
                   });

                   $scope.livestock_submit = function () {
                       var lslivestock_gid = '';
                       var lslivestock_name = '';

                       if ($scope.cbolivestockname != undefined || $scope.cbolivestockname != null) {
                           lslivestock_gid = $scope.cbolivestockname.livestock_gid;
                           lslivestock_name = $scope.cbolivestockname.livestock_name;
                       }

                       var params = {
                           livestock_gid: lslivestock_gid,
                           livestock_name: lslivestock_name,
                           count: $scope.txtcount,
                           breed: $scope.txtbreed,
                           insurance_status: $scope.rdbinsurancestatus,
                           insurance_details: $scope.txtlivestockinsurance_details
                       }
                       var url = 'api/MstCADApplication/PostGroupLivestock';
                       lockUI();
                       SocketService.post(url, params).then(function (resp) {
                           unlockUI();
                           if (resp.data.status == true) {

                               Notify.alert(resp.data.message, {
                                   status: 'success',
                                   pos: 'top-center',
                                   timeout: 3000
                               });
                               livestock_list();
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

                   $scope.ok = function () {
                       $modalInstance.close('closed');
                   };
               }
           }

           $scope.deletelivestock_holding = function (group2livestock_gid) {
               var params = {
                   group2livestock_gid: group2livestock_gid
               }
               var url = 'api/MstCADApplication/DeleteGroupLivestock';
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
                   livestock_list();
               });

           }

           $scope.livestock_View = function (group2livestock_gid) {
               var modalInstance = $modal.open({
                   templateUrl: '/LiveStockHoldingView.html',
                   controller: ModalInstanceCtrl,
                   backdrop: 'static',
                   keyboard: false,
                   size: 'lg'
               });
               ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
               function ModalInstanceCtrl($scope, $modalInstance) {
                   var params = {
                       group2livestock_gid: group2livestock_gid
                   }
                   var url = 'api/MstCADApplication/GetGroupLivestockHoldingView';
                   lockUI();
                   SocketService.getparams(url, params).then(function (resp) {
                       unlockUI();
                       $scope.lblbreed = resp.data.Breed;
                       $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                   });

                   $scope.ok = function () {
                       $modalInstance.close('closed');
                   };
               }
           }

           function livestock_list() {
               var params = {
                   group_gid: $scope.group_gid
               }
               var url = 'api/MstCADApplication/GetEditGroupLivestockList';
               SocketService.getparams(url, params).then(function (resp) {
                   $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;

               });
           }

           $scope.downloadall = function () {
               for (var i = 0; i < $scope.groupdocument_list.length; i++) {
                   DownloaddocumentService.Downloaddocument($scope.groupdocument_list[i].document_path, $scope.groupdocument_list[i].document_name);
               }
           }
    }
})();

