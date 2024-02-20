(function () {
    'use strict';

    angular
           .module('angle')
           .controller('AgrMstGroupTabController', AgrMstGroupTabController);

           AgrMstGroupTabController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrMstGroupTabController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstGroupTabController';
        var application_gid = $location.search().lsapplication_gid;
        var application_gid = $scope.application_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;
        activate();

        function activate() {

            var url = 'api/AgrMstApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblvertical_name = resp.data.vertical_name;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.application_gid = resp.data.application_gid;
                $scope.application_status = resp.data.application_status;
                $scope.applicant_type = resp.data.applicant_type;
                });

                var url = 'api/AgrMstApplicationAdd/GetAppProductcharges';
                SocketService.get(url).then(function (resp) {
                    $scope.economical_flag = resp.data.economical_flag;
                  
                    if ($scope.economical_flag == 'Y') {
                        $scope.social_tradetab = false;
                        $scope.social_trade = true;
                    }
                    else {
                        $scope.social_tradetab = true;
                        $scope.social_trade = false;
                    }
                });

                var url = 'api/AgrMstApplicationAdd/GetGroupTempClear';
                SocketService.get(url).then(function (resp) {
                });

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


            $scope.URN_yes = false;

            var url = 'api/AgrMstApplication360/GroupDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.groupdoc_list = resp.data.groupdoc_list;
            });

            var url = 'api/AgrMstApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {

                }
            });

        }

        $scope.onselectedurn_yes = function () {
            if ($scope.rdbgroupurn_status == 'Yes') {
                $scope.URN_yes = true;
            }
            else {
                $scope.URN_yes = false;
            }
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

        function groupaddress_list() {
            var url = 'api/AgrMstApplicationAdd/GetGroupAddressList';
            SocketService.get(url).then(function (resp) {
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
                        address_typegid: $scope.cboaddresstype.address_gid,
                        address_type: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
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
                            groupaddress_list();
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
                    groupaddress_list();
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
                    groupbank_list();
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

        $scope.bank_delete = function (group2bank_gid) {
            var params =
                {
                    group2bank_gid: group2bank_gid
                }
           
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

                groupbank_list();
            });

        }

        function groupbank_list() {
            var url = 'api/AgrMstApplicationAdd/GetGroupBankList';
            SocketService.get(url).then(function (resp) {
                $scope.mstbank_list = resp.data.mstbank_list;

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

                        groupdocument_list();

                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        function groupdocument_list() {
            var url = 'api/AgrMstApplicationAdd/GetGroupDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.groupdocument_list = resp.data.groupdocument_list;
            });
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
                groupdocument_list();
                unlockUI();
            });
        }

        $scope.group_save = function () {

            var params = {
                group_name: $scope.txtgroup_name,
                date_of_formation: $scope.txtdate_of_formation,
                group_type: $scope.cboGroupType,
                groupmember_count: $scope.txtgroupmember_count,
                groupurn_status: $scope.rdbgroupurn_status,
                group_urn: $scope.txtgroup_urn,
            }

            var url = 'api/AgrMstApplicationAdd/GroupSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });        
                    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
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

            var params = {
                group_name: $scope.txtgroup_name,
                date_of_formation: $scope.txtdate_of_formation,
                group_type: $scope.cboGroupType,
                groupmember_count: $scope.txtgroupmember_count,
                groupurn_status: $scope.rdbgroupurn_status,
                group_urn: $scope.txtgroup_urn,
            }

            var url = 'api/AgrMstApplicationAdd/GroupSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
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
        $scope.Back = function () {
            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
        }
        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/AgrMstApplicationGeneralAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/AgrMstApplicationInstitutionAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstApplicationIndividualAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.Individual_dtls=true;
                    }
                }

        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstApplicationGroupAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.BureauUpdates_dtls=true;
                    }
                }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstApplicationSocialTradeCapitalAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.EconomicCapital_dtls=true;
                }
            }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.OverallLimit_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstApplicationOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ProductCharges_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstApplicationProductChargesAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ServiceCharges_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstApplicationServiceChargeAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.Hypothecation_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
            else {
                 $scope.BureauUpdates_dtls=true;
                }
            }

        $scope.doneclick = function () {
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {

                }
            });
        }
        $scope.overallsubmit_application = function () {

            var params = {

            }
            var url = 'api/AgrMstApplicationAdd/PostAppProceed';
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
                $state.go('app.MstApplicationCreationSummary');
            });

        }
        $scope.hierarchy_change = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyChange.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            var application_gid = $scope.application_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstApplicationAdd/FnKycDcoumentValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else {

                    }

                });


                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyChangeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rm_name = resp.data.rm_name;
                    $scope.directreportingto_name = resp.data.directreportingto_name;
                    $scope.clustermanager_gid = resp.data.clustermanager_gid;
                    $scope.clustermanager_name = resp.data.clustermanager_name;
                    $scope.regionalhead_gid = resp.data.regionalhead_gid;
                    $scope.regionhead_name = resp.data.regionhead_name;
                    $scope.zonalhead_gid = resp.data.zonalhead_gid;
                    $scope.zonalhead_name = resp.data.zonalhead_name;
                    $scope.businesshead_gid = resp.data.businesshead_gid;
                    $scope.businesshead_name = resp.data.businesshead_name;
                });

                $scope.Update_hierarchy = function () {
                    var params = {
                        application_gid: application_gid,
                        clustermanager_gid: $scope.clustermanager_gid,
                        clustermanager_name: $scope.clustermanager_name,
                        regionalhead_gid: $scope.regionalhead_gid,
                        regionalhead_name: $scope.regionhead_name,
                        zonalhead_gid: $scope.zonalhead_gid,
                        zonalhead_name: $scope.zonalhead_name,
                        businesshead_gid: $scope.businesshead_gid,
                        businesshead_name: $scope.businesshead_name
                    }
                    var url = 'api/AgrMstApplicationAdd/UpdateApprovalHierarchyChange';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                    $modalInstance.close('closed');
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.groupdocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.groupdocument_list[i].document_path, $scope.groupdocument_list[i].document_name);
            }
        }

    }
})();
