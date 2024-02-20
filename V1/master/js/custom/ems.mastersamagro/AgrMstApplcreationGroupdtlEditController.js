
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplcreationGroupdtlEditController', AgrMstApplcreationGroupdtlEditController);

    AgrMstApplcreationGroupdtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrMstApplcreationGroupdtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplcreationGroupdtlEditController';

        $scope.group_gid = $location.search().lsgroup_gid;
        var lstab = $location.search().lstab;
        
        activate();
        function activate() {
            lockUI();

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

            var url = 'api/AgrMstApplicationAdd/GetGroupTempClear';
            SocketService.get(url).then(function (resp) {
            });
   
            var param = {
                group_gid: $scope.group_gid
            };

           /* var url = 'api/AgrMstApplicationEdit/GetApplicationBasicDetailsTempClear';
            SocketService.get(url).then(function (resp) {
            }); */


            var url = 'api/AgrMstApplicationEdit/EditGroup';


            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtdate_of_formation = resp.data.date_of_formation;
                $scope.cboGroupType = resp.data.group_type;
                $scope.txtgroupmember_count = resp.data.groupmember_count;
                $scope.rdbgroupurn_status = resp.data.groupurn_status;
                $scope.txtgroup_urn = resp.data.group_urn;
                $scope.group_status = resp.data.group_status;
                if (resp.data.group_status == "Incomplete") {
                    $scope.GroupSubmit = true;
                    $scope.GroupUpdate = false;
                }
                else {
                    $scope.GroupSubmit = false;
                    $scope.GroupUpdate = true;
                }

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

                unlockUI();
            });

            var url = 'api/AgrMstApplicationEdit/GroupDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.groupdocument_list = resp.data.groupdocument_list;
            });

            var url = 'api/AgrMstApplicationEdit/GroupBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstbank_list = resp.data.mstbank_list;
            });

            var url = 'api/AgrMstApplicationEdit/GroupAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstaddress_list = resp.data.mstaddress_list;

            });

            var url = 'api/AgrMstApplication360/GroupDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.groupdoc_list = resp.data.groupdoc_list;
            });

        }

        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/AgrMstApplicationAdd/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
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

                var url = 'api/AgrKyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {

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

            var url = 'api/AgrKyc/BankAccVerification';
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
            var url = 'api/AgrMstApplicationEdit/GroupAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
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
                $scope.geocodingFailed = false;

                var url = 'api/AgrMstAddressType/GetAddressTypeASC';
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
                    var url = 'api/AgrMstbuyer/GetPostalCodeDetails';

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
                    else if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/AgrGoogleMapsAPI/GetGeoCoding';
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
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/AgrMstApplicationAdd/PostGroupAddressDetail';
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
                var url = 'api/AgrMstAddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    group2address_gid: group2address_gid
                }
                var url = 'api/AgrMstApplicationAdd/EditGroupAddressDetail';
                SocketService.getparams(url, params).then(function (resp) {

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
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/AgrMstbuyer/GetPostalCodeDetails';

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
                    else if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/AgrGoogleMapsAPI/GetGeoCoding';
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
                $scope.address_update = function () {
                    var address_Type = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboAddressType,
                        address_type: address_Type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        group2address_gid: group2address_gid,
                        group_gid: $scope.group_gid,
                    }
                    var url = 'api/AgrMstApplicationAdd/UpdateGroupAddressDetail';
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
            var url = 'api/AgrMstApplicationAdd/DeleteGroupAddressDetail';
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
                var url = 'api/AgrGoogleMapsAPI/GetStaticMapUrl';
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
                var url = 'api/AgrGoogleMapsAPI/GetPlaceImage';
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

        function groupbanktemp_list() {
            var param = {
                group_gid: $scope.group_gid
            };
            var url = 'api/AgrMstApplicationEdit/GroupBankTmpList';
            SocketService.getparams(url, param).then(function (resp) {
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
                }
                var url = 'api/AgrMstApplicationAdd/PostGroupBankDetail';
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
                var url = 'api/AgrMstApplicationAdd/EditGroupBankDetail';
                SocketService.getparams(url, params).then(function (resp) {

                    
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
                        group_gid: $scope.group_gid,
                        group2bank_gid: $scope.group2bank_gid,
                  
                    }
                    var url = 'api/AgrMstApplicationAdd/UpdateGroupBankDetail';
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
            var url = 'api/AgrMstApplicationAdd/DeleteGroupBankDetail';
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

                groupbanktemp_list();
            });

        }


        function groupdocumenttemp_list() {
            var param = {
                group_gid: $scope.group_gid
            };
            var url = 'api/AgrMstApplicationEdit/GroupDocumentTmpList';
            SocketService.getparams(url, param).then(function (resp) {
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/AgrMstApplicationAdd/GroupDocumentUpload';
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


        $scope.groupdocument_delete = function (group2document_gid) {

            var params = {
                group2document_gid: group2document_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GroupDocumentDelete';
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
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.group_save = function () {
            var dateParts, dateObject;
            try {
                dateParts = $scope.txtdate_of_formation.split("-");
                dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            }
            catch (err) {
                dateObject = $scope.txtdate_of_formation;
            }
            var params = {
                group_name: $scope.txtgroup_name,
                date_of_formation: dateObject,
                group_type: $scope.cboGroupType,
                groupmember_count: $scope.txtgroupmember_count,
                groupurn_status: $scope.rdbgroupurn_status,
                group_urn: $scope.txtgroup_urn,
                group_gid: $scope.group_gid
            }

            var url = 'api/AgrMstApplicationEdit/SaveGroupDtlEdit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lstab == 'add') {
                        $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstApplicationGeneralEdit');
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

        $scope.group_submit = function () {

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
                var params = {
                    group_name: $scope.txtgroup_name,
                    date_of_formation: dateObject,
                    group_type: $scope.cboGroupType,
                    groupmember_count: $scope.txtgroupmember_count,
                    groupurn_status: $scope.rdbgroupurn_status,
                    group_urn: $scope.txtgroup_urn,
                    group_gid: $scope.group_gid
                }

                var url = 'api/AgrMstApplicationEdit/SubmitGroupEditDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lstab == 'add') {
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstApplicationGeneralEdit');
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
                var params = {
                    group_name: $scope.txtgroup_name,
                    date_of_formation: dateObject,
                    group_type: $scope.cboGroupType,
                    groupmember_count: $scope.txtgroupmember_count,
                    groupurn_status: $scope.rdbgroupurn_status,
                    group_urn: $scope.txtgroup_urn,
                    group_gid: $scope.group_gid
                }

                var url = 'api/AgrMstApplicationEdit/UpdateGroupDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lstab == 'add') {
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstApplicationGeneralEdit');
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
            if (lstab == 'add') {
                $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.AgrMstApplicationGeneralEdit');
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.groupdocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.groupdocument_list[i].document_path, $scope.groupdocument_list[i].document_name);
            }
        }

    }
})();

