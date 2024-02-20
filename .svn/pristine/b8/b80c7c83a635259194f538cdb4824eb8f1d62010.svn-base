(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditorMasterEditController', AgrMstCreditorMasterEditController);

    AgrMstCreditorMasterEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCreditorMasterEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditorMasterEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.creditor_gid = searchObject.creditor_gid;

        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        //lockUI();
        function activate() {

            var creditor_id = $scope.creditor_gid;

            $scope.input = false
            $scope.span = true
            $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false
            $scope.dateyes = true

            vm.calender11 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open11 = true;
            };
            // Calender Popup... //

            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open12 = true;
            };

            vm.calender13 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open13 = true;
            };

            vm.calender14 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open14 = true;
            };

            vm.calender15 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open15 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/AgrMstCreditorMaster/GetCreditorTmpClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstApplication360/GetOtherCreditorApplicantTypedropdown';
            SocketService.get(url).then(function (resp) {
                $scope.othercreditorapplicanttype_data = resp.data.application_list;
            });

            var url = 'api/UdcManagement/GetDropDownUdc';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankname_list = resp.data.bankname_list;
                unlockUI();
            });


            var url = 'api/AgrDesignation/Getdesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            //var url = 'api/AgrMstWarehouseAdd/GetTmpSpocEmployee';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.employee_list = resp.data.employeedtl;

            //});

            //var url = 'api/AgrMstApplicationAdd/Getproduct';
            //SocketService.getparams(url, param).then(function (resp) {
            //    unlockUI();
            //    $scope.product_list = resp.data.product_list;
            //});

            //////var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
            //////SocketService.get(url).then(function (resp) {
            //////    unlockUI();
            //////    $scope.loanproductlist = resp.data.loanproductlist;
            //////});

           
            var params = {
                warehouse_gid: ''
            }
            var url = 'api/AgrMstWarehouseAdd/GetTmpSpocEmployee';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_list = resp.data.employeedtl;

            });
            var params = {
                creditor_gid: $scope.creditor_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) { 
                $scope.loanproductlist = resp.data.loanproductlist;
            });
            var url = 'api/AgrMstCreditorMaster/CreditorSubProductEdit';
            SocketService.getparams(url, params).then(function (resp) { 
                $scope.loansubproductlist = resp.data.creditorprogram_list;
            });

            $scope.arrlist = [{
                "id": "0",
                "category": "Entity"

            }, {
                "id": "1",
                "category": "Individual"
            },
             {
                 "id": "2",
                 "category": ""
             }];

         

            var url = 'api/AgrMstCreditorMaster/EditCreditorDetails';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtapplicant_name = resp.data.Applicant_name;
                $scope.txtcreditor_code = resp.data.creditorref_no;
                //$scope.rdeditowned_by = resp.data.Applicant_type;
               /* $scope.cboProductTypelist = resp.data.loanproduct_gid;*/
                //$scope.applicant_category = resp.data.Applicant_category;
                //$scope.cboProductSubTypelist = resp.data.loansubproduct_gid;
                $scope.cboapplicanttype_name = resp.data.Applicanttype_gid;
                $scope.cbodesignation_type = resp.data.designation_gid;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtcontact_number = resp.data.contact_no;
                $scope.applicant_category = resp.data.Applicant_category;
                var getselected = $scope.arrlist.find(function (a) { return a.category === $scope.applicant_category })
                if (getselected != null) {
                    $scope.applicant_editcategory = getselected.id
                }
                if ($scope.applicant_category == 'Entity') {

                    $scope.Entity = true;
                    $scope.Individual = false;

                }
                else if ($scope.applicant_category == 'Individual') {

                    $scope.Individual = true;
                    $scope.Entity = false;

                }
                $scope.txtmail_id = resp.data.email_id;
                $scope.txtaadhar_no = resp.data.aadhar_no;
              
             
                unlockUI();
            });

            var url = 'api/AgrMstCreditorMaster/CreditorSubProductEdit';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loansubproductlist = resp.data.creditorprogram_list;
            });


            var url = 'api/AgrMstCreditorMaster/creditorAddressTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditoraddress_list = resp.data.creditoraddress_list;
            });

            var url = 'api/AgrMstCreditorMaster/creditorGSTTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutiongst_list = resp.data.creditorgst_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetcreditorAgreementDetailsTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlcreditoragreementdtllist;

            });

            var url = 'api/AgrMstCreditorMaster/GetChequeSummaryTmplist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.creditorcheque_list;

            });
             
            var url = 'api/AgrMstCreditorMaster/CreditorProductEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboProductTypelist = [];
                if (resp.data.creditorproduct_list != null) {
                    $scope.creditorproductedit_list = resp.data.creditorproduct_list;
                    var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
                    SocketService.get(url).then(function (resp) {
                        $scope.loanproductlist = resp.data.loanproductlist;
                        if ($scope.loanproductlist != undefined) {
                            var count = $scope.creditorproductedit_list.length;
                            for (var i = 0; i < count; i++) {
                                var indexs = $scope.loanproductlist.map(function (x) { return x.loanproduct_gid; }).indexOf($scope.creditorproductedit_list[i].loanproduct_gid);
                                $scope.cboProductTypelist.push($scope.loanproductlist[indexs]);
                            }
                        }   
                    });
                    
                }
               
            });

            var url = 'api/AgrMstCreditorMaster/CreditorProgramEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboProductSubTypelist = [];
                if (resp.data.creditorprogram_list != null) {
                    $scope.creditorprogramedit_list = resp.data.creditorprogram_list;
                    var url = 'api/AgrMstCreditorMaster/CreditorSubProductEdit';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.creditorprogram_list;
                        if ($scope.loansubproductlist != undefined) {
                    var count = $scope.creditorprogramedit_list.length; 
                            for (var j = 0; j < count; j++) {
                                var indexs = $scope.loansubproductlist.map(function (x) { return x.loansubproduct_gid; }).indexOf($scope.creditorprogramedit_list[j].loansubproduct_gid);
                                $scope.cboProductSubTypelist.push($scope.loansubproductlist[indexs]);
                            }
                        }

                    });
                }

            });
   

        }

        //$scope.producttype = function () {
        //    var params = {
        //        loanproduct_gid: $scope.cboProductTypelist, application_gid: '',
        //        application2loan_gid: ''
        //    }
        //    var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.loansubproductlist = resp.data.application_list;
        //    });
        //}


        $scope.producttype = function () {
            var params = {
                multipleloanproduct_list: $scope.cboProductTypelist
            }
            var url = 'api/AgrMstApplicationAdd/GetMultipleLoanSubProduct';
            SocketService.post(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.multipleloansubproduct_list;
            });
        }

        $scope.show = function () {

            if ($scope.inputType == 'password')
                $scope.inputType = 'text';
            $scope.eye = false
            $scope.eyeslash = true
            //else
            //    $scope.inputType = 'password';

        }

        $scope.noshow = function () {

            if ($scope.inputType == 'text')
                $scope.inputType = 'password';
            //else
            //    $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false

        }

        $scope.onchangecategory = function (applicant_category) {

            var getselected = $scope.arrlist.find(function (a) { return a.id === $scope.applicant_editcategory })
            if (getselected != null) {
                $scope.applicant_category = getselected.category
            }
            if ($scope.applicant_category == 'Entity') {

                $scope.Entity = true;
                $scope.Individual = false;

            }
            else if ($scope.applicant_category == 'Individual') {

                $scope.Individual = true;
                $scope.Entity = false;

            }

            else {

                //$scope.Entity = false;
                //$scope.Individual = false;

            }
        }

        $scope.download_doc = function (val1, val2) {

            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_number;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/AgrMstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_number.length == 10) {
                if ($scope.institutiongst_list != null) {
                    var paramsdel =
                    {
                        creditor_gid: $scope.creditor_gid
                    }
                    var url = 'api/AgrMstCreditorMaster/DeleteGSTCreditor';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtpan_number
                }
                var url = 'api/AgrKyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/AgrMstCreditorMaster/PostCreditorMasterGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                creditorgstlist();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/AgrKyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                                creditorgstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                creditorgstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.headoffice_confirm = function (creditor2branch_gid, creditor_id) {
            var modalInstance = $modal.open({
                templateUrl: '/HeadOfficeConfirmation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.headoffice_submit = function () {
                    var params = {
                        creditor2branch_gid: creditor2branch_gid,
                        creditor_gid: creditor_id
                    }
                    lockUI();
                    var url = 'api/AgrMstCreditorMaster/UpdateGSTHeadOfficeEdit';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            creditorgstlist();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                        }
                    });
                }

          


            }

        }
        $scope.institutiongst_add = function () {
            if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
                Notify.alert('Enter GST State / Select GST Registered Status / GST Number', 'warning');
            }
            else {
                var params = {
                    gst_state: $scope.txtgst_state,
                    gst_no: $scope.txtgst_number,
                    gst_registered: $scope.rdbgstregistered
                }
                var url = 'api/AgrMstCreditorMaster/PostCreditorMasterGST';
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
                    creditorgstlist();
                    $scope.txtgst_state = '';
                    $scope.txtgst_number = '';
                    $scope.rdbgstregistered = '';
                });
            }
        }

        $scope.institutiongst_delete = function (creditor2branch_gid) {
            var params =
                {
                    creditor2branch_gid: creditor2branch_gid
                }
            var url = 'api/AgrMstCreditorMaster/DeleteCreditorMasterGST';
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
                creditorgstlist();
            });

        }

        function creditorgstlist() {
            var url = 'api/AgrMstCreditorMaster/GetCreditorMasterGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.institutiongst_list = resp.data.creditorgst_list;
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
                    var url = 'api/AgrMstCreditorMaster/PostcreditorAddressDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            creditoraddress_list();
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

        $scope.address_delete = function (creditor2address_gid) {
            var params =
                {
                    creditor2address_gid: creditor2address_gid
                }
            var url = 'api/AgrMstCreditorMaster/DeletecreditorAddressDetail';
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
                creditoraddress_list();
            });

        }

        function creditoraddress_list() {
            var url = 'api/AgrMstCreditorMaster/GetcreditorAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.creditoraddress_list = resp.data.creditoraddress_list;
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

        $scope.add_cheque = function () {

            if (($scope.txtaccount_number == undefined) || ($scope.txtaccount_number == '') || ($scope.txtcheque_no == undefined) || ($scope.txtcheque_no == '') ||
                ($scope.txtaccountholder_name == undefined) || ($scope.txtaccountholder_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbank_name == '') ||
                ($scope.txtifsc_code == undefined) || ($scope.txtifsc_code == '') || ($scope.txtmicr == undefined) || ($scope.txtmicr == '') ||
                ($scope.txtbranch_address == undefined) || ($scope.txtbranch_address == '') || ($scope.txtcity == undefined) || ($scope.txtcity == '') ||
                ($scope.txtbranch_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtcity == undefined) || ($scope.txtcity == '') ||
                ($scope.txtdistrict == undefined) || ($scope.txtdistrict == '') || ($scope.txtstate == undefined) || ($scope.txtstate == '') || ($scope.rdbprimarystatus == '') || ($scope.rdbprimarystatus == undefined)

            )
            {
                Notify.alert('Enter Cheque Details', 'warning');
            }

            //else if (($scope.txtdate_chequetype == undefined) || ($scope.txtdate_chequetype == '') || ($scope.txtdate_chequetype == "") ||
            //    ($scope.rbocts_enabled == undefined) || ($scope.rbocts_enabled == '') || ($scope.rbocts_enabled == "") || ($scope.txtdate_chequepresentation == "") || ($scope.txtdate_chequepresentation == undefined) || ($scope.txtdate_chequepresentation == '') || ($scope.txtdate_chequepresentation == "")||
            //    ($scope.txtdate_chequeclearance == undefined) || ($scope.txtdate_chequeclearance == '') || ($scope.txtdate_chequeclearance == "") || ($scope.txtstatus_chequeclearance == undefined) || ($scope.txtstatus_chequeclearance == '') || ($scope.txtstatus_chequeclearance == "") ||
            //    ($scope.txtgeneral_remarks == undefined) || ($scope.txtgeneral_remarks == '') || ($scope.txtgeneral_remarks == "")) {
            //    Notify.alert('Enter All Mandatory Cheque Details', 'warning');
            //}

            else {

                var lsbankname_gid = '';
                var lsbankname_name = '';

                if ($scope.cbomergedbanking_entity != undefined || $scope.cbomergedbanking_entity != null) {
                    lsbankname_gid = $scope.cbomergedbanking_entity.bankname_gid;
                    lsbankname_name = $scope.cbomergedbanking_entity.bankname_name;
                }

                var params = {

                    accountholder_name: $scope.txtaccountholder_name,
                    account_number: $scope.txtaccount_number,
                    bank_name: $scope.txtbank_name,
                    cheque_no: $scope.txtcheque_no,
                    ifsc_code: $scope.txtifsc_code,
                    micr: $scope.txtmicr,
                    branch_address: $scope.txtbranch_address,
                    branch_name: $scope.txtbranch_name,
                    city: $scope.txtcity,
                    district: $scope.txtdistrict,
                    state: $scope.txtstate,
                    //mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                    //mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                    mergedbankingentity_gid: lsbankname_gid,
                    mergedbankingentity_name: lsbankname_name,
                    special_condition: $scope.txtspecial_condition,
                    general_remarks: $scope.txtgeneral_remarks,
                    cts_enabled: $scope.rbocts_enabled,
                    cheque_type: $scope.cbocheque_type,
                    date_chequetype: $scope.txtdate_chequetype,
                    date_chequepresentation: $scope.txtdate_chequepresentation,
                    status_chequepresentation: $scope.txtstatus_chequepresentation,
                    date_chequeclearance: $scope.txtdate_chequeclearance,
                    status_chequeclearance: $scope.txtstatus_chequeclearance,
                    primary_status: $scope.rdbprimarystatus,
                    creditor_gid: $scope.creditor_gid

                }


                var url = 'api/AgrMstCreditorMaster/PostChequeDetail';
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
                    cheque_list();
                    $scope.span = true
                    //  $scope.cboStakeholder = '',
                    //$scope.txtstakeholder_type = '',
                    //$scope.txtdesignation = '',
                    $scope.txtaccountholder_name = '',
                    $scope.txtaccount_number = '',
                    $scope.txtbank_name = "",
                    $scope.txtcheque_no = "",
                    $scope.txtifsc_code = "",
                    $scope.txtmicr = "",
                    $scope.txtbranch_address = "",
                    $scope.txtbranch_name = "",
                    $scope.txtcity = "",
                    $scope.txtdistrict = "",
                    $scope.txtstate = "",
                    $scope.cbomergedbanking_entity = "",
                    $scope.txtspecial_condition = "",
                    $scope.txtgeneral_remarks = "",
                    $scope.rbocts_enabled = "",
                    $scope.cbocheque_type = "",
                    $scope.txtdate_chequetype = "",
                    $scope.txtdate_chequepresentation = "",
                    $scope.txtstatus_chequepresentation = "",
                    $scope.txtdate_chequeclearance = "",
                    $scope.txtstatus_chequeclearance = "",
                    $scope.rdbprimarystatus = ""
                    $scope.uploadfrm = undefined;
                    $scope.chequedocument_list = null;


                });
            }


        }


        function cheque_list() {

            var params = {
                creditor_gid: $scope.creditor_gid
            }

            var url = 'api/AgrMstCreditorMaster/GetChequeSummaryTmplist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.creditorcheque_list;
            });

        }




        $scope.delete_cheque = function (creditor2cheque_gid) {
            lockUI();
            var params = {
                creditor2cheque_gid: creditor2cheque_gid
            }
            var url = 'api/AgrMstCreditorMaster/DeleteChequeDetail';
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();

                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

        }

        function chequedocument_list() {
            var url = 'api/AgrMstCreditorMaster/GetChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.chequedocument_list = resp.data.creditorchequedocument_list;

                if ($scope.chequedocument_list == null) {
                    $scope.input = false
                    $scope.span = true
                }

            });
        }

        $scope.UploadDocument = function (val, val1, name) {

            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }

            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/AgrMstCreditorMaster/ChequeDocumentUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        var url = 'api/AgrKyc/ChequeOCR';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            if (resp.data.statusCode == 101) {
                                $scope.txtaccountholder_name = resp.data.result.name[0];
                                $scope.txtaccount_number = resp.data.result.accNo;
                                $scope.txtbank_name = resp.data.result.bank;
                                $scope.txtcheque_no = resp.data.result.chequeNo;
                                $scope.txtifsc_code = resp.data.result.ifsc;
                                $scope.txtmicr = resp.data.result.micr;
                                $scope.txtbranch_address = resp.data.result.bankDetails.address;
                                $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                                $scope.txtcity = resp.data.result.bankDetails.city;
                                $scope.txtdistrict = resp.data.result.bankDetails.district;
                                $scope.txtstate = resp.data.result.bankDetails.state;
                            }
                            else {
                                Notify.alert('Document uploaded Successfully but Error occured while fetching values from document..!', 'warning');
                                $scope.input = false
                                $scope.span = true
                            }
                        });

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.input = true
                        $scope.span = false

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $("#file").val('');
                    $scope.uploadfrm = undefined;

                    chequedocument_list();
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }

        }

        $scope.delete_document = function (creditorcheque2document_gid) {
            lockUI();
            var params = {
                creditorcheque2document_gid: creditorcheque2document_gid
            }
            var url = 'api/AgrMstCreditorMaster/ChequeDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentupload_list = resp.data.documentupload_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.input = false
                    $scope.span = true
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                chequedocument_list();
                $scope.txtaccountholder_name = '',
                    $scope.txtaccount_number = '',
                    $scope.txtbank_name = "",
                    $scope.txtcheque_no = "",
                    $scope.txtifsc_code = "",
                    $scope.txtmicr = "",
                    $scope.txtbranch_address = "",
                    $scope.txtbranch_name = "",
                    $scope.txtcity = "",
                    $scope.txtdistrict = "",
                    $scope.txtstate = "",
                    $scope.cbomergedbanking_entity = "",
                    $scope.txtspecial_condition = "",
                    $scope.txtgeneral_remarks = "",
                    $scope.rbocts_enabled = "",
                    $scope.cbocheque_type = "",
                    $scope.txtdate_chequetype = "",
                    $scope.txtdate_chequepresentation = "",
                    $scope.txtstatus_chequepresentation = "",
                    $scope.txtdate_chequeclearance = "",
                    $scope.txtstatus_chequeclearance = "",
                    $scope.rdbprimarystatus = ""

                unlockUI();
            });
        }


        $scope.onchangeyesno = function (rdexecute_app) {
            if ($scope.rdexecute_app == 'Yes') {
                $scope.date = false;
                $scope.dateyes = true;
                $scope.dateno = false;

            }
            else if ($scope.rdexecute_app == 'No') {
                $scope.date = true;
                $scope.dateyes = false;
                $scope.dateno = true;
                $scope.txtend_date = "",
                $scope.txtstart_date = ""
            }

            else {

                //$scope.Entity = false;
                //$scope.Individual = false;

            }
        }


        $scope.add_agreement = function () {
            if ($scope.cboemployee == undefined || $scope.cboemployee == null || $scope.cboemployee == "" || $scope.rdexecute_app == undefined || $scope.rdexecute_app == null || $scope.rdexecute_app == "" ) {
                Notify.alert('Kindly fill Agreement the Details', 'warning')
            }

            if ($scope.rdexecute_app == 'Yes') {

                if ($scope.txtend_date == undefined || $scope.txtend_date == null || $scope.txtend_date == "") {
                    Notify.alert('Kindly fill Agreement Expiry Date', 'warning')
                }
                else if ($scope.txtstart_date == undefined || $scope.txtstart_date == null || $scope.txtstart_date == "") {
                    Notify.alert('Kindly fill Agreement Execution Date', 'warning')
                }
            }

                var params = {
                    samcontact_perssonid: $scope.cboemployee.employee_gid,
                    samcontact_perssonname: $scope.cboemployee.employee_name,
                    creditor2agreement_no: $scope.txtagreement_no,
                    agreementinvolvement_type: $scope.rdexecute_app,
                    execution_date: $scope.txtstart_date,
                    expiry_date: $scope.txtend_date,
                }
                var url = 'api/AgrMstCreditorMaster/PostcreditorAgreementDetails';
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

                    agreement_list();
                    $scope.cboagreementaddress = '';
                    $scope.txtstart_date = '';
                    $scope.txtend_date = '';
                    $scope.rdexecute_app = '';
                    $scope.txtagreement_no = '';
                    $scope.cboemployee = '';
                });
            
        }

        function agreement_list() {

            var params = {
                creditor_gid: $scope.creditor_gid
            };

            var url = 'api/AgrMstCreditorMaster/GetcreditorAgreementDetailsTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlcreditoragreementdtllist;

            });
        }

        $scope.agreementdoc_upload = function (creditor2agreement_gid, creditor_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.txtwarehousedocument_name = document_title;
                var params = {
                    creditor2agreement_gid: creditor2agreement_gid,
                }
                var url = 'api/AgrMstCreditorMaster/Getcreditordocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;

                });


                $scope.creditordocumentupload = function (val) {
                    if (($scope.txtcreditordocument_name == null) || ($scope.txtcreditordocument_name == '') || ($scope.txtcreditordocument_name == undefined)) {
                        $("#momdocument").val('');
                        Notify.alert('Kindly Enter the Document Title', 'warning');
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
                                $modalInstance.close('closed');
                                return false;
                            }

                        }
                        frm.append('document_title', $scope.txtcreditordocument_name);
                        frm.append('creditor_gid', creditor_gid);
                        frm.append('creditor2agreement_gid', creditor2agreement_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstCreditorMaster/creditorDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;
                                unlockUI();

                                $("#institutionfile").val('');
                                $scope.uploadfrm = undefined;
                                $scope.txtcreditordocument_name = '';
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
                                unlockUI();
                            });
                        }
                        else {
                            alert('Document is not Available..!');
                            return;
                        }
                    }
                }

                $scope.institutiondocument_delete = function (creditoragreement2docupload_gid) {
                    lockUI();
                    var params = {
                        creditoragreement2docupload_gid: creditoragreement2docupload_gid,
                        creditor2agreement_gid: creditor2agreement_gid
                    }
                    var url = 'api/AgrMstCreditorMaster/creditordoc_delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;
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
                        unlockUI();
                    });
                }

                $scope.download_doc = function (val1, val2) {

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');

                }
                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
                    }
                }

            }
        }

        $scope.agreementAdd_delete = function (creditor_gid, creditor2agreement_gid) {
            lockUI();
            var params = {
                creditor2agreement_gid: creditor2agreement_gid
            }
            var url = 'api/AgrMstCreditorMaster/DeleteAgreementDetail';
            SocketService.getparams(url, params).then(function (resp) {
                agreement_list();
                //$scope.institutionupload_list = resp.data.agrmstwarhouse_upload;
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
                unlockUI();
            });
        }

        $scope.submit = function () {

            //var lswarehousefacilties_gid = '';
            //var lswarehouse_facility = '';

            //if ($scope.cbowarehouse_facility == undefined || $scope.cbowarehouse_facility == null || $scope.cbowarehouse_facility == "") {
            //    Notify.alert('Kindly select Warehouse Facilities', 'warning')
            //}
            //else if ($scope.rdowned_by == '' || $scope.rdowned_by == undefined || $scope.rdowned_by == null) {
            //    Notify.alert('Kindly Enter Owned by', 'warning')
            //}
            //else {
            var lsloanproduct_name = "", lsloanproduct_gid = "";
            var lsloansubproduct_gid = "", lsloansubproduct_name = "";
            var lsdesignation_gid = "", lsdesignation_type = "";
            if ($scope.cboProductTypelist != null) {
                lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
            }
            if ($scope.cboProductSubTypelist != null) {
                lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
            }
            if ($scope.cbodesignation_type != null) {
                lsdesignation_gid = $scope.cbodesignation_type.designation_gid;
                lsdesignation_type = $scope.cbodesignation_type.designation_type;
            }
            var params = {
                pan_no: $scope.txtpan_number,
                creditorref_no: $scope.txtcreditor_code,
                contactperson_name: $scope.txtcontactperson_name,
                contact_no: $scope.txtcontact_number,
                email_id: $scope.txtmail_id,

                //warehousefacilties_gid: lswarehousefacilties_gid,
                //warehouse_facility: lswarehouse_facility,
                //loanproduct_gid: lsloanproduct_gid,
                //loanproduct_name: lsloanproduct_name,
                //loansubproduct_gid: lsloansubproduct_gid,

                //loansubproduct_name: lsloansubproduct_name,
                multiloanproduct_list: $scope.cboProductTypelist,
                multiloansubproduct_list: $scope.cboProductSubTypelist,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,

            }
            var url = 'api/AgrMstCreditorMaster/PostCreditorSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$location.url('app/AgrMstCreditorMasterSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
            //}

        }

        $scope.Back = function () {

            if (lspage == "Approved") {
                $state.go('app.AgrMstCreditorMasterApproval');
            }

            else {

                $state.go('app.AgrMstCreditorMasterSummary');

            }
        }

        $scope.edit_cheque = function (creditor2cheque_gid) {

            if (lspage == "Approved") {
                $location.url('app/AgrMstCreditorMasterChequeEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + creditor2cheque_gid + '&lspage=Approved'));
            }

            else {

                $location.url('app/AgrMstCreditorMasterChequeEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + $scope.creditor_gid + '&creditor2cheque_gid=' + creditor2cheque_gid));

            }
        }
        $scope.Update = function () {
            var Gstflag = 'Yes';
            var lsloanproduct_name = "", lsloanproduct_gid = "";
            var lsloansubproduct_gid = "", lsloansubproduct_name = "";
            var lsdesignation_gid = "", lsdesignation_type = "";
            var lsapplicanttype_gid = "", lsapplicanttype_name = "";

            if ($scope.cboProductTypelist.length == '0') {

                Notify.alert("Kindly Tag the Product", 'warning');
                //unlockUI();
                return false;

            }
            else if ($scope.cboProductSubTypelist.length == '0') {

                Notify.alert("Kindly Tag the Program", 'warning');
                //unlockUI();
                return false;

            }

            else {

             

                if ($scope.cboProductTypelist != null) {
                    lsloanproduct_gid = $scope.cboProductTypelist;
                    lsloanproduct_name = $scope.loanproductlist.find(function (x) { return x.loanproduct_gid === $scope.cboProductTypelist });
                    if (lsloanproduct_name)
                        lsloanproduct_name = lsloanproduct_name.loanproduct_name;
                }
                if ($scope.cboProductSubTypelist != null) {
                    lsloansubproduct_gid = $scope.cboProductSubTypelist;
                    lsloansubproduct_name = $scope.loansubproductlist.find(function (x) { return x.loansubproduct_gid === $scope.cboProductSubTypelist });
                    if (lsloansubproduct_name)
                        lsloansubproduct_name = lsloansubproduct_name.loansubproduct_name;
                }
                if ($scope.cbodesignation_type != null) {
                    lsdesignation_gid = $scope.cbodesignation_type;
                    lsdesignation_type = $scope.designation_list.find(function (x) { return x.designation_gid === $scope.cbodesignation_type });
                    if (lsdesignation_type)
                        lsdesignation_type = lsdesignation_type.designation_type;
                }

                if ($scope.cboapplicanttype_name != null) {


                    creditorgstlist();

                    //var url = 'api/AgrMstCreditorMaster/GetCreditorMasterGSTList';
                    //lockUI();
                    //SocketService.get(url).then(function (resp) {

                    //    $scope.institutiongst_list = resp.data.creditorgst_list;
                    //    if (resp.data.status == true) {
                            if ($scope.institutiongst_list != null) {
                                $scope.Gstflag = 'Yes';
                            }
                            else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                                $scope.Gstflag = 'No';
                            }
                    //    }
                        
                    //});

                    lsapplicanttype_gid = $scope.cboapplicanttype_name;
                    lsapplicanttype_name = $scope.othercreditorapplicanttype_data.find(function (x) { return x.othercreditorapplicanttype_gid === $scope.cboapplicanttype_name })
                    if (lsapplicanttype_name)
                        lsapplicanttype_name = lsapplicanttype_name.othercreditorapplicanttype_name;
                    //unlockUI();
                }

              


                var params = {
                    pan_no: $scope.txtpan_number,
                    creditorref_no: $scope.txtcreditor_code,
                    contactperson_name: $scope.txtcontactperson_name,
                    contact_no: $scope.txtcontact_number,
                    email_id: $scope.txtmail_id,
                    Applicant_name: $scope.txtapplicant_name,
                    //warehousefacilties_gid: lswarehousefacilties_gid,
                    //warehouse_facility: lswarehouse_facility,
                    //loanproduct_gid: lsloanproduct_gid,
                    //loanproduct_name: lsloanproduct_name,
                    //loansubproduct_gid: lsloansubproduct_gid,

                    //loansubproduct_name: lsloansubproduct_name,
                    multiloanproduct_list: $scope.cboProductTypelist,
                    multiloansubproduct_list: $scope.cboProductSubTypelist,
                    Applicant_type: lsapplicanttype_name,
                    Applicanttype_gid: lsapplicanttype_gid,
                    Applicant_category: $scope.applicant_category,
                    designation_gid: lsdesignation_gid,
                    designation_type: lsdesignation_type,
                    aadhar_no: $scope.txtaadhar_no,
                    creditor_gid: $scope.creditor_gid,
                    Gstflag: $scope.Gstflag
                }
               
                var url = 'api/AgrMstCreditorMaster/UpdatecreditorDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lspage == "Approved") {
                            $state.go('app.AgrMstCreditorMasterApproval');
                        }

                        else {

                            $state.go('app.AgrMstCreditorMasterSummary');

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

        $scope.edit_gstdetails = function (creditor2branch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/GSTdetailedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditor2branch_gid: creditor2branch_gid
                }
                var url = 'api/AgrMstCreditorMaster/EditCreditorMasterGST';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.rdbgstregistered = resp.data.gst_registered;
                    $scope.txteditgst_state = resp.data.gst_state;
                    $scope.txteditgst_number = resp.data.gst_no;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangeeditgst_number = function () {
                    var gst_number = $scope.txteditgst_number;
                    var params = {
                        gst_code: gst_number.substring(0, 2)
                    }
                    var url = 'api/AgrMstApplicationAdd/GetGSTState';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditgst_state = resp.data.gst_state;
                    });
                }

                $scope.update_gst = function () {
                    var params = {
                        gst_state: $scope.txteditgst_state,
                        gst_no: $scope.txteditgst_number,
                        gst_registered: $scope.rdbgstregistered,
                        creditor2branch_gid: creditor2branch_gid,
                        creditor_gid: $location.search().creditor_gid
                    }
                    var url = 'api/AgrMstCreditorMaster/UpdateCreditorMasterGST';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            creditorgstlist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        creditorgstlist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        function creditorgstlist() {
            var params = {
                creditor_gid: $scope.creditor_gid
            }

            var url = 'api/AgrMstCreditorMaster/creditorGSTTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutiongst_list = resp.data.creditorgst_list;
            });
        }


        $scope.editinstitution_address = function (creditor2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editaddresstype.html',
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
                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });

                var params = {
                    creditor2address_gid: creditor2address_gid
                }
                var url = 'api/AgrMstCreditorMaster/EditcreditorAddressDetail';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboaddresstype_GID = resp.data.address_typegid;
                    $scope.rdbprimaryaddress = resp.data.primary_status;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtLand_Mark = resp.data.landmark;
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
                $scope.update_address = function () {
                    var addresstype = $('#address_type :selected').text();
                    var params = {
                        address_typegid: $scope.cboaddresstype_GID,
                        address_type: addresstype,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        landmark: $scope.txtLand_Mark,
                        creditor2address_gid: creditor2address_gid,
                        creditor_gid: $location.search().creditor_gid
                    }
                    var url = 'api/AgrMstCreditorMaster/UpdatecreditorAddressDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            institutionaddresslist();
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


        function institutionaddresslist() {
            var params = {
                creditor_gid: $scope.creditor_gid
            };
            var url = 'api/AgrMstCreditorMaster/creditorAddressTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditoraddress_list = resp.data.creditoraddress_list;
            });
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cheque_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cheque_list[i].document_path, $scope.cheque_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
            }
        }

        //$scope.v = function (creditor2cheque_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/chequeedit.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

     
        //        var params = {
        //            creditor2cheque_gid: creditor2cheque_gid
        //        }
        //        var url = 'api/AgrMstCreditorMaster/ChequeDetailsEdit';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txtaccountholder_name = resp.data.accountholder_name;
        //            $scope.txtaccount_number = resp.data.account_number;
        //            $scope.txtbank_name = resp.data.bank_name;
        //            $scope.txtcheque_no = resp.data.cheque_no;
        //            $scope.txtifsc_code = resp.data.ifsc_code;
        //            $scope.txtmicr = resp.data.micr;
        //            $scope.txtbranch_address = resp.data.branch_address;
        //            $scope.txtbranch_name = resp.data.branch_name;
        //            $scope.txtcity = resp.data.city;
        //            $scope.txtdistrict = resp.data.district;
        //            $scope.txtstate = resp.data.state;
        //            $scope.cbomergedbanking_entity = resp.data.mergedbankingentity_gid;
        //            $scope.cbocheque_type = resp.data.cheque_type;

        //            $scope.txtdate_chequetype = resp.data.datechequetype;
        //            //$scope.txtdate_chequetype = Date.parse($scope.txtdate_chequetype);

        //            $scope.rbocts_enabled = resp.data.cts_enabled;

        //            $scope.txtdate_chequepresentation = resp.data.datechequepresentation;
        //            //$scope.txtdate_chequepresentation = Date.parse($scope.txtdate_chequepresentation);

        //            $scope.txtstatus_chequepresentation = resp.data.status_chequepresentation;

        //            $scope.txtdate_chequeclearance = resp.data.datechequeclearance;
        //            //$scope.txtdate_chequeclearance = Date.parse($scope.txtdate_chequeclearance);

        //            $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
        //            $scope.txtspecial_condition = resp.data.special_condition;
        //            $scope.txtgeneral_remarks = resp.data.general_remarks;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update_cheque = function () {

        //            var mergedbankingentity_Name = $('#mergedbanking_entity :selected').text();

        //            var params = {
        //                mergedbankingentity_gid: $scope.cbomergedbanking_entity,
        //                mergedbankingentity_name: mergedbankingentity_Name,
        //                special_condition: $scope.txtspecial_condition,
        //                general_remarks: $scope.txtgeneral_remarks,
        //                cts_enabled: $scope.rbocts_enabled,
        //                cheque_type: $scope.cbocheque_type,
        //                date_chequetype: $scope.txtdate_chequetype,
        //                date_chequepresentation: $scope.txtdate_chequepresentation,
        //                status_chequepresentation: $scope.txtstatus_chequepresentation,
        //                date_chequeclearance: $scope.txtdate_chequeclearance,
        //                status_chequeclearance: $scope.txtstatus_chequeclearance,
        //                creditor2cheque_gid: $scope.creditor2cheque_gid
        //            }
        //            var url = 'api/AgrMstCreditorMaster/UpdateChequeDetail';
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //            });

        //        }

        //    }
        //}

    }
})();
