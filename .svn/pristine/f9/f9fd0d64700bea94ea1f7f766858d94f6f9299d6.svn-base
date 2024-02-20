(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationGroupEditController', MstApplicationGroupEditController);

    MstApplicationGroupEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstApplicationGroupEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationGroupEditController';


        activate();
        var application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;
        function activate() {

            $scope.application_gid = $location.search().lsapplication_gid;
            
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

            var url = 'api/MstApplication360/GetInternalRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.internalrating_list = resp.data.internalrating_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });

            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });

            var url = 'api/MstApplicationAdd/GetGroupTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lblprocessing_fee = resp.data.processing_fee;
                $scope.lbldoc_charges = resp.data.doc_charges;
                $scope.application_gid = resp.data.application_gid;
                $scope.applicant_type = resp.data.applicant_type;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.productcharge_flag = resp.data.productcharge_flag;
                $scope.economical_flag = resp.data.economical_flag;
                $scope.lblproductcharges_status = resp.data.productcharges_status;
                $scope.application_status = resp.data.application_status;

                if ($scope.economical_flag == 'N') {
                    $scope.social_tradetab = false;
                    $scope.social_trade = true;
                }
                else {
                    $scope.social_tradetab = true;
                    $scope.social_trade = false;
                }

                if ($scope.productcharge_flag == 'N') {
                    $scope.product_chargetab = false;
                    $scope.product_charge = true;
                }
                else {
                    $scope.product_chargetab = true;
                    $scope.product_charge = false;
                }
            });

            var url = 'api/MstApplication360/GroupDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.groupdoc_list = resp.data.groupdoc_list;
            });

        }

          //Group

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
              var url = 'api/MstApplicationAdd/FutureDateCheck';
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

                  var url = 'api/Kyc/IfscVerification';
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

                  var url = 'api/Kyc/BankAccVerification';
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
            var url = 'api/MstApplicationAdd/GetGroupAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.mstaddress_list = resp.data.mstaddress_list;
            });
        }

        $scope.groupaddress_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/groupaddresstype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
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
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined){
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
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
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
                $scope.groupaddressSubmit = function () {

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
                    var url = 'api/MstApplicationAdd/PostGroupAddressDetail';
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

        $scope.groupaddress_delete = function (group2address_gid) {
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
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
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
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
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
                var url = 'api/MstApplicationAdd/PostGroupBankDetail';
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
            console.log(params)
            var url = 'api/MstApplicationAdd/DeleteGroupBankDetail';
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
            var url = 'api/MstApplicationAdd/GetGroupBankList';
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

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstApplicationAdd/GroupDocumentUpload';
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

        function groupdocument_list() {
            var url = 'api/MstApplicationAdd/GetGroupDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.groupdocument_list = resp.data.groupdocument_list;
            });
        }

        $scope.groupdocument_delete = function (group2document_gid) {

            var params = {
                group2document_gid: group2document_gid
            }
            lockUI();
            var url = 'api/MstApplicationAdd/GroupDocumentDelete';
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
            var dateParts, dateObject;
            try {
                dateParts = $scope.txtdate_of_formation.split("-");
                dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            }
            catch (err) {
                dateObject = $scope.txtdate_of_formation;
            }
            var dateParts, dateObject;
            try {
                dateParts = $scope.txtdate_of_formation.split("-");
                dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            }
            catch (err) {
                dateObject = $scope.txtdate_of_formation;
            }
            var lsinternalrating_gid = '';
            var lsinternalrating_name = '';

            if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
            }
            var params = {
                group_name: $scope.txtgroup_name,
                date_of_formation: dateObject,
                group_type: $scope.cboGroupType,
                groupmember_count: $scope.txtgroupmember_count,
                groupurn_status: $scope.rdbgroupurn_status,
                group_urn: $scope.txtgroup_urn,
                group_gid: $scope.group_gid,
                application_gid: $scope.application_gid,
                male_count: $scope.txtmale_count,
                female_count: $scope.txtfemale_count,
                internalrating_gid: lsinternalrating_gid,
                internalrating_name: lsinternalrating_name
            }

            var url = 'api/MstApplicationEdit/SaveGroupDtlAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
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
            var dateParts, dateObject;
            try {
                dateParts = $scope.txtdate_of_formation.split("-");
                dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            }
            catch (err) {
                dateObject = $scope.txtdate_of_formation;
            }
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

                var lsinternalrating_gid = '';
                var lsinternalrating_name = '';

                if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                    lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                    lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
                }

                var params = {
                    group_name: $scope.txtgroup_name,
                    date_of_formation: dateObject,
                    group_type: $scope.cboGroupType,
                    groupmember_count: $scope.txtgroupmember_count,
                    groupurn_status: $scope.rdbgroupurn_status,
                    group_urn: $scope.txtgroup_urn,
                    group_gid: $scope.group_gid,
                    application_gid: $scope.application_gid,
                    male_count: $scope.txtmale_count,
                    female_count: $scope.txtfemale_count,
                    internalrating_gid: lsinternalrating_gid,
                    internalrating_name: lsinternalrating_name
                }

                var url = 'api/MstApplicationEdit/SubmitGroupDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
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
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/MstApplicationProductChargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        var params = {
            application_gid: $scope.application_gid
        }

        $scope.doneclick = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }           
            var url = 'api/MstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {              
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });
        }

        $scope.overallsubmit_application = function () {
            var url = 'api/MstApplicationEdit/EditAppProceed';
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
                $state.go('app.MstApplicationCreationSummary');
            });

        }

        $scope.submit = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/EditAppReProceed';            
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
                $state.go('app.MstApplicationCreationSummary');
            });

        }

        $scope.Back = function () {
            $state.go('app.MstApplicationCreationSummary');
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

                var url = 'api/MstApplicationAdd/FnKycDcoumentValidation';
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


                var url = 'api/MstApplicationAdd/GetApprovalHierarchyChangeList';
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
                    var url = 'api/MstApplicationAdd/UpdateApprovalHierarchyChange';
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
                    var url = 'api/MstApplicationAdd/PostGroupEquipmentHolding';
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
            var url = 'api/MstApplicationAdd/DeleteGroupEquipmentHolding';
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
                var url = 'api/MstApplicationAdd/GetGroupEquipmentHoldingView';
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
            var url = 'api/MstApplicationAdd/GetGroupEquipmentHoldingList';
            SocketService.get(url).then(function (resp) {
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
                    var url = 'api/MstApplicationAdd/PostGroupLivestock';
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
            var url = 'api/MstApplicationAdd/DeleteGroupLivestock';
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
                var url = 'api/MstApplicationAdd/GetGroupLivestockHoldingView';
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
            var url = 'api/MstApplicationAdd/GetGroupLivestockList';
            SocketService.get(url).then(function (resp) {
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

