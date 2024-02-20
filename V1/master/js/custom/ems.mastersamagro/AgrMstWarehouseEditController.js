(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstWarehouseEditController', AgrMstWarehouseEditController);

    AgrMstWarehouseEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstWarehouseEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstWarehouseEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.warehouse_gid = searchObject.warehouse_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        var lblGstflag;
        activate();
        //lockUI();
        function activate() {

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

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.wareshow = false;
            $scope.capacityshow = false;
            $scope.volumeshow = false;
            var url = 'api/AgrMstWarehouseEdit/GetWarehouseTmpClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AgrMstUom/GetuomList';
            SocketService.get(url).then(function (resp) {
                $scope.uom_list = resp.data.Uomdtl;
            });
            var url = 'api/AgrMstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.productname_list = resp.data.productname_list;
            });

            var params = {
                product_gid: $scope.product_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
            unlockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.varietyname_list = resp.data.varietyname_list;
            });

            var param = {
                warehouse_gid: $scope.warehouse_gid
            }
            var url = 'api/AgrMstWarehouseAdd/GetTmpSpocEmployee';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_list = resp.data.employeedtl;

            });

            //var url = 'api/AgrMstWarehouseAdd/GetWarehouseAddressList';
            //unlockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.agreementaddress_list = resp.data.agrmstaddress_list;
            //});

            var param = {
                warehouse_gid: $scope.warehouse_gid
            };

            var url = 'api/AgrMstWarehouseEdit/warehouseGSTTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutiongst_list = resp.data.agrmstgst_list;
            });


            var param = {
                warehouse_gid: $scope.warehouse_gid
            };

            var url = 'api/AgrMstWarehouseEdit/warehouseAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.agreementaddress_list = resp.data.agrmstaddress_list;
            })

            var url = 'api/AgrMstWarehouseEdit/WarehouseAgreementDetailsTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlagrmstagreementdtllist;

            });

            var url = 'api/AgrMstWarehouseEdit/warehouseMobileNoTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.agrmstmobileno_list;
            });

            var url = 'api/AgrMstWarehouseEdit/warehouseEmailAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.agrmstemailaddress_list;
            });

            //var url = 'api/AgrMstWarehouseEdit/warehouseAddressTmpList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.contactaddress_list = resp.data.agrmstaddress_list;
            //});

            var url = 'api/AgrMstWarehouseEdit/WarehouseCommodityTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactidproof_list = resp.data.Warehousevarietyname_list;
            });

            var url = 'api/AgrMstWarehouseEdit/GetWarehouseDocument';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
            });

            var url = 'api/AgrMstWarehouseEdit/warehouseSpocTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Warehousespoc_list = resp.data.Warehousespoc_list;
            });


            var url = 'api/AgrMstApplication360/GetWarehouseFacility';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.facility_list = resp.data.application_list;
            });
            var url = 'api/AgrMstWarehouseAdd/Gettypeofwarehouse';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.typeofwarehouse_data = resp.data.warehousetype_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetApprovedcreditorSummary';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

            var url = 'api/AgrMstWarehouseAdd/EditWarehouseDetails';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtpan_number = resp.data.warehouse_pan;
                $scope.txteditfirst_name = resp.data.first_name;
                $scope.txteditmiddle_name = resp.data.middle_name;
                $scope.txteditlast_name = resp.data.last_name;
                $scope.txteditwarehouse_name = resp.data.warehouse_name;
                $scope.txteditwarehouse_ref_no = resp.data.warehouse_ref_no;
                $scope.rdeditowned_by = resp.data.owned_by;
                if ($scope.rdeditowned_by == 'Third-party') {
                    $scope.enable = true;
                }
                else {
                    $scope.enable = false;
                    $scope.cboApplicant_name = '';
                }

                $scope.txteditsubsidiarywarshouse_name = resp.data.subsidiarywarshouse_name;
                $scope.txteditwarehouse_area = resp.data.warehouse_area;
                $scope.txteditwarehousearea_uom = resp.data.warehousearea_uom;
                $scope.txtedittotalcapacity_area = resp.data.totalcapacity_area;
                $scope.txteditarea_uom = resp.data.area_uom;
                $scope.txtedittotalcapacity_volume = resp.data.totalcapacity_volume;
                $scope.txteditvolume_uom = resp.data.volume_uom;
                $scope.txteditcharges = resp.data.charges;
                $scope.txteditcapacity = resp.data.capacity;
                $scope.warehousetype_name = resp.data.typeofwarehouse_gid;
                $scope.cboApplicant_name = resp.data.creditor_gid;
                $scope.cboeditwarehousearea_uom = resp.data.warehousearea_uomgid;
                $scope.cboeditarea_uom = resp.data.totalcapacityarea_uomgid;
                $scope.cboeditvolume_uom = resp.data.volume_uomgid;
                if ($scope.txteditwarehouse_area != null && $scope.txteditwarehouse_area != "")
                    $scope.wareshow = true;
                else
                    $scope.wareshow = false;
                if ($scope.txtedittotalcapacity_area != null && $scope.txtedittotalcapacity_area != "") 
                    $scope.capacityshow = true;
                else
                    $scope.capacityshow = false;
                if ($scope.txtedittotalcapacity_volume != null && $scope.txtedittotalcapacity_volume != "")
                    $scope.volumeshow = true;
                else
                    $scope.volumeshow = false;
                

                var url = "api/AgrMstWarehouseAdd/WarehouseFacilityEdit";
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cbowarehouse_facility = [];
                    if (resp.data.facility_list != null) {
                        $scope.warehousefacilityedit_list = resp.data.facility_list;
                        var count = $scope.warehousefacilityedit_list.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.facility_list.map(function (x) { return x.warehousefacility_gid; }).indexOf($scope.warehousefacilityedit_list[i].warehousefacility_gid);
                            $scope.cbowarehouse_facility.push($scope.facility_list[indexs]);
                            $scope.cbowarehouse_facility = $scope.cbowarehouse_facility;
                        }
                    }
                });
                unlockUI();
            });

        }

        $scope.onselected = function () {
            if ($scope.rdeditowned_by == 'Third-party') {
                $scope.enable = true;
            }
            else {
                $scope.enable = false;
                $scope.cboApplicant_name = '';
            }
        }


        function institutiongstlist() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/warehouseGSTTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutiongst_list = resp.data.agrmstgst_list;
            });
        }

        function institutionmobilenolist() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/warehouseMobileNoTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.agrmstmobileno_list;
            });
        }

        function institutionmail_list() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/warehouseEmailAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.agrmstemailaddress_list;
            });
        }

        function institutionaddresslist() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/warehouseAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.agreementaddress_list = resp.data.agrmstaddress_list;
            });
        }


        function institutionspoc_list() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/warehouseSpocTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Warehousespoc_list = resp.data.Warehousespoc_list;
            });
            var url = 'api/AgrMstWarehouseAdd/GetTmpSpocEmployee';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_list = resp.data.employeedtl;
            });
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
                        warehouse_gid: $scope.warehouse_gid
                    }
                    var url = 'api/AgrMstWarehouseAdd/DeleteGSTWarehouse';
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

                        var url = 'api/AgrMstWarehouseAdd/PostWarehouseGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                institutiongstlist();
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
                            if(resp.data.result != null) {
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.panvalidation = true;
                                    institutiongstlist();
                                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                    $scope.panvalidation = false;
                                    Notify.alert('PAN is not verified..!', 'warning');
                                    institutiongstlist();
                                } 
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
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
                var url = 'api/AgrMstWarehouseAdd/PostWarehouseGST';
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
                    institutiongstlist();
                    $scope.txtgst_state = '';
                    $scope.txtgst_number = '';
                    $scope.rdbgstregistered = '';
                });
            }
        }

        $scope.edit_gstdetails = function (warehouse2branch_gid) {
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
                    warehouse2branch_gid: warehouse2branch_gid
                }
                var url = 'api/AgrMstWarehouseAdd/EditWarehouseGST';
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
                        warehouse2branch_gid: warehouse2branch_gid,
                        warehouse_gid: $location.search().warehouse_gid
                    }
                    var url = 'api/AgrMstWarehouseAdd/UpdateWarehouseGST';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            institutiongstlist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        institutiongstlist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.institutiongst_delete = function (warehouse2branch_gid) {
            var params =
                {
                    warehouse2branch_gid: warehouse2branch_gid
                }
            var url = 'api/AgrMstWarehouseAdd/DeleteWarehouseGST';
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
                institutiongstlist();
            });

        }

        $scope.addinstitutionaddress = function () {
            var modalInstance = $modal.open({
                templateUrl: '/institutionaddresstype.html',
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
                $scope.institutionaddressSubmit = function () {

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
                    var url = 'api/AgrMstWarehouseAdd/PostWarehouseAddressDetail';
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

        $scope.deleteinstitution_address = function (warehouse2address_gid) {
            var params =
                {
                    warehouse2address_gid: warehouse2address_gid
                }
            var url = 'api/AgrMstWarehouseAdd/DeleteWarehouseAddressDetail';
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
                institutionaddresslist();
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

        $scope.agreementdoc_upload = function (warehouse2agreement_gid, warehouse_gid) {
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
                    warehouse2agreement_gid: warehouse2agreement_gid,
                }
                var url = 'api/AgrMstWarehouseEdit/WarehouseDocumentUploadTmpList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;

                });

                $scope.warehousdocumentupload = function (val) {
                    if (($scope.txtwarehousedocument_name == null) || ($scope.txtwarehousedocument_name == '') || ($scope.txtwarehousedocument_name == undefined)) {
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
                                return false;
                            }

                        }
                        frm.append('document_title', $scope.txtwarehousedocument_name);
                        frm.append('warehouse_gid', $scope.warehouse_gid);
                        frm.append('warehouseagreement_gid', warehouse2agreement_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                        console.log("FormData", frm);
                        console.log("variable", $scope.uploadfrm);
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstWarehouseAdd/WarehouseDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
                                unlockUI();
                                $scope.txtwarehousedocument_name = '';
                                $("#institutionfile").val('');
                                $scope.uploadfrm = undefined;

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

                $scope.institutiondocument_delete = function (warehouse2docupload_gid) {
                    lockUI();
                    var params = {
                        warehouse2docupload_gid: warehouse2docupload_gid,
                        warehouse2agreement_gid: warehouse2agreement_gid
                    }
                    var url = 'api/AgrMstWarehouseAdd/warehousedoc_delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
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
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
                    }
                }

            }
        }


        $scope.agreementAdd_delete = function (warehouse_gid, warehouse2agreement_gid) {
            lockUI();
            var params = {
                warehouse2agreement_gid: warehouse2agreement_gid
            }
            var url = 'api/AgrMstWarehouseAdd/DeleteAgreementDetail';
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

        $scope.institutionmobileno_add = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined) || ($scope.rdbprimarymobile_no == '') || ($scope.rdbprimarywhatsapp_no == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimarymobile_no,
                    whatsapp_no: $scope.rdbprimarywhatsapp_no
                }
                var url = 'api/AgrMstWarehouseAdd/PostWarehouseMobileNo';
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
                    institutionmobilenolist();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbprimarywhatsapp_no = '';
                });
            }
        }

        $scope.institutionmobileno_delete = function (warehouse2mobileno_gid) {
            var params =
                {
                    warehouse2mobileno_gid: warehouse2mobileno_gid
                }
            var url = 'api/AgrMstWarehouseAdd/DeleteWarehouseMobileNo';
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
                institutionmobilenolist();
            });

        }

        $scope.add_institutiomaildetails = function () {
            if (($scope.txtinstitutionmail_address == undefined) || ($scope.txtinstitutionmail_address == '') || ($scope.rdbinstitutiomaildetails == undefined) || ($scope.rdbinstitutiomaildetails == '')) {
                Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtinstitutionmail_address,
                    primary_status: $scope.rdbinstitutiomaildetails
                }
                var url = 'api/AgrMstWarehouseAdd/PostWarehouseEmailAddress';
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
                    institutionmail_list();
                    $scope.txtinstitutionmail_address = '';
                    $scope.rdbinstitutiomaildetails = '';
                });
            }
        }

        $scope.institutionmail_delete = function (warehouse2email_gid) {
            var params =
                {
                    warehouse2email_gid: warehouse2email_gid
                }
            var url = 'api/AgrMstWarehouseAdd/DeletewarehouseEmailAddress';
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

                institutionmail_list();
            });

        }

        $scope.Warehousespoc_delete = function (warehouse2spoc_gid) {
            var params =
                {
                    warehouse2spoc_gid: warehouse2spoc_gid
                }
            var url = 'api/AgrMstWarehouseAdd/DeleteWarehousespoc';
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

                institutionspoc_list();
            });

        }


        //$scope.futuredatecheck = function (val) {
        //    var params = {
        //        date: val.toDateString()
        //    }
        //    var url = 'api/AgrMstWarehouseAdd/FutureDateCheck';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == false) {
        //            Notify.alert(resp.data.message, 'warning')
        //        }
        //    });
        //}

        $scope.productname_change = function (cboproduct_name) {
            var params = {
                product_gid: $scope.cboproduct_name.product_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessunit_gid = resp.data.businessunit_gid;
                $scope.txtsector_name = resp.data.businessunit_name;
                $scope.valuechain_gid = resp.data.valuechain_gid;
                $scope.txtcategory_name = resp.data.valuechain_name;
                $scope.varietyname_list = resp.data.varietyname_list;
            });
            $scope.txtbotanical_name = '';
            $scope.txtalternative_name = '';
        }

        $scope.Variety_change = function (cbovariety_name) {
            var params = {
                variety_gid: $scope.cbovariety_name.variety_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txthsn_code = resp.data.hsn_code;
            });
        }

        $scope.onchangewarearea = function () {
           
            if ($scope.txteditwarehouse_area != null && $scope.txteditwarehouse_area != "") {

                $scope.wareshow = true;
            }
            else {
                $scope.wareshow = false;
            }
        }

        $scope.onchangecapacity = function () {

            if ($scope.txtedittotalcapacity_area != null && $scope.txtedittotalcapacity_area != "") {

                $scope.capacityshow = true;
            }
            else
                $scope.capacityshow = false;
        }

        $scope.onchangevolume = function () {

            if ($scope.txtedittotalcapacity_volume != null && $scope.txtedittotalcapacity_volume != "") {

                $scope.volumeshow = true;
            }
            else
                $scope.volumeshow = false;
        }


        $scope.otherdetails_add = function () {

            var lsproduct_name = '';
            var lsproduct_gid = '';
            var lsvariety_name = '';
            var lsvariety_gid = '';


            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                lsproduct_name = $scope.cboproduct_name.product_name;
                lsproduct_gid = $scope.cboproduct_name.product_gid;
            }
            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                lsvariety_name = $scope.cbovariety_name.variety_name;
                lsvariety_gid = $scope.cbovariety_name.variety_gid;
            }

            var params = {
                product_gid: lsproduct_gid,
                product_name: lsproduct_name,
                variety_gid: lsvariety_gid,
                variety_name: lsvariety_name,
                sector_name: $scope.txtsector_name,
                category_name: $scope.txtcategory_name,
                botanical_name: $scope.txtbotanical_name,
                alternative_name: $scope.txtalternative_name
            }
            var url = 'api/AgrMstWarehouseAdd/PostWarehouseCommodity';
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
                commodity_list();
                $scope.cboproduct_name = '';
                $scope.txtsector_name = '';
                $scope.txtcategory_name = '';
                $scope.cbovariety_name = '';
                $scope.txtbotanical_name = '';
                $scope.txtalternative_name = '';

            });
        }


        $scope.bank_delete = function (warehouse2commodity_gid) {
            var params =
                {
                    warehouse2commodity_gid: warehouse2commodity_gid
                }
            console.log(params)
            var url = 'api/AgrMstWarehouseAdd/DeleteWarehouseCommodity';
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

                commodity_list();
            });

        }

        function commodity_list() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/WarehouseCommodityTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactidproof_list = resp.data.Warehousevarietyname_list;

            });
        }

        //$scope.onchangespocname = function (cbospoc_name) {
        //    var params = {
        //        spocid_list: $scope.cbospoc_name,
        //    }
        //    var url = 'api/AgrMstWarehouseAdd/Getspocno';
        //    SocketService.post(url, params).then(function (resp) {
        //        $scope.spoc_phoneno = resp.data.spoc_phoneno;
        //    });

        //}

        $scope.onchangespocname = function (cbospoc_name) {

            var lsemployee_gid = '';
            var lsemployee_name = '';
            if ($scope.cbospoc_name != undefined || $scope.cbospoc_name != null) {
                lsemployee_gid = $scope.cbospoc_name.employee_gid;
                //lsemployee_name = $scope.cbospoc_name.employee_name;
            }

            var params = {
                lsemployee_gid: lsemployee_gid,
            }
            var url = 'api/AgrMstWarehouseAdd/Getspocno';
            SocketService.post(url, params).then(function (resp) {
                $scope.spoc_phoneno = resp.data.spoc_phoneno;
            });

        }

        $scope.add_spocdetails = function () {

            var params = {
                spoc_id: $scope.cbospoc_name.employee_gid,
                spoc_name: $scope.cbospoc_name.employee_name,
                spocmobile_no: $scope.spoc_phoneno
            }
            var url = 'api/AgrMstWarehouseAdd/PostWarehouseSpocDetails';
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
                institutionspoc_list();
                $scope.cbospoc_name = '';
                $scope.spoc_phoneno = '';
            });
        }

        $scope.add_agreement = function () {
            if ($scope.txtend_date == undefined || $scope.txtend_date == null || $scope.txtend_date == "") {
                Notify.alert('Kindly fill Agreement Expiry Date', 'warning')
            }
            else if ($scope.txtstart_date == undefined || $scope.txtstart_date == null || $scope.txtstart_date == "") {
                Notify.alert('Kindly fill Agreement Execution Date', 'warning')
            }
            else {
                var params = {
                    warehouse2address_gid: $scope.cboagreementaddress.warehouse2address_gid,
                    warehouse_address: $scope.cboagreementaddress.addressline1,
                    execution_date: $scope.txtstart_date,
                    expiry_date: $scope.txtend_date,
                }
                var url = 'api/AgrMstWarehouseAdd/PostWarehouseAgreementDetails';
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
                    $scope.warehouse_address = '';
                    $scope.txtstart_date = '';
                    $scope.txtend_date = '';
                });
            }

        }

        function agreement_list() {
            var param = {
                warehouse_gid: $scope.warehouse_gid
            };
            var url = 'api/AgrMstWarehouseEdit/WarehouseAgreementDetailsTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlagrmstagreementdtllist;

            });
        }

        $scope.Back = function () {


            if (lspage == "WarehouseEdit") {
                $state.go('app.AgrMstWarehouseCreationSummary');
            }
            else if (lspage == "ProductApproval") {
                $state.go('app.AgrMstPendingProductApproval');
            }
            else if (lspage == "PMGApproval") {
                $state.go('app.AgrMstPendingPmgApproval');
            }
            else if (lspage == "ApprovedWarehouse") {
                $state.go('app.AgrMstWarehouseAprovalSummary');
            }
            else {

            }
        }


        $scope.warehouse_update = function () {

            //var lswarehousefacilties_gid = '';
            //var lswarehouse_facility = '';

            institutiongstlist();
            if ($scope.cbowarehouse_facility == undefined || $scope.cbowarehouse_facility == null || $scope.cbowarehouse_facility == "") {
                Notify.alert('Kindly select Warehouse Facilities', 'warning')
            }
           
           
            //else if ($scope.rdeditowned_by == '' || $scope.rdeditowned_by == undefined || $scope.rdeditowned_by == null) {
            //    Notify.alert('Kindly Enter Owned By', 'warning')
            //}
            else {
                var lswarehousearea_uomgid = "", lswarehousearea_uom = "";
                var lsareacapacity_uomgid = "", lsareacapacity_uom = "";
                var lsvolume_uomgid = "", lsvolume_uom = "";
                var lstypeofwarehouse_gid = "", lstypeofwarehouse_name = "";
                var lscreditor_gid = "", lsapplicant_name = "";
               
                if ($scope.institutiongst_list == undefined || $scope.institutiongst_list == null || $scope.institutiongst_list == "") {
                    lblGstflag = 'No';
                }
                else {
                    lblGstflag = 'Yes';
                }
                if ($scope.cboeditwarehousearea_uom != null) {
                    lswarehousearea_uomgid = $scope.cboeditwarehousearea_uom;
                    lswarehousearea_uom = $scope.uom_list.find(function (x) { return x.uom_gid === $scope.cboeditwarehousearea_uom })
                    if (lswarehousearea_uom)
                        lswarehousearea_uom = lswarehousearea_uom.uom_name; 
                }
                if ($scope.cboeditarea_uom != null) {
                    lsareacapacity_uomgid = $scope.cboeditarea_uom;
                    lsareacapacity_uom = $scope.uom_list.find(function (x) { return x.uom_gid === $scope.cboeditarea_uom })
                    if (lsareacapacity_uom)
                        lsareacapacity_uom = lsareacapacity_uom.uom_name; 
                }
                if ($scope.cboeditvolume_uom != null) {
                    lsvolume_uomgid = $scope.cboeditvolume_uom; 
                    lsvolume_uom = $scope.uom_list.find(function (x) { return x.uom_gid === $scope.cboeditvolume_uom })
                    if (lsvolume_uom)
                        lsvolume_uom = lsvolume_uom.uom_name; 
                }
                if ($scope.warehousetype_name != null) {
                    lstypeofwarehouse_gid = $scope.warehousetype_name;
                    lstypeofwarehouse_name = $scope.typeofwarehouse_data.find(function (x) { return x.typeofwarehouse_gid === $scope.warehousetype_name })
                    if (lstypeofwarehouse_name)
                        lstypeofwarehouse_name = lstypeofwarehouse_name.typeofwarehouse_name;
                }

                if ($scope.cboApplicant_name != null) {
                    lscreditor_gid = $scope.cboApplicant_name;
                    lsapplicant_name = $scope.creditoradd_list.find(function (x) { return x.creditor_gid === $scope.cboApplicant_name})
                    if (lsapplicant_name)
                        lsapplicant_name = lsapplicant_name.Applicant_name;
                }

                var params = {

                    warehouse_ref_no: $scope.txteditwarehouse_ref_no,
                    first_name: $scope.txteditfirst_name,
                    middle_name: $scope.txteditmiddle_name,
                    last_name: $scope.txteditlast_name,
                    facility_list: $scope.cbowarehouse_facility,
                    warehouse_name: $scope.txteditwarehouse_name,
                    //owned_by: $scope.rdeditowned_by,

                    warehouse_pan: $scope.txtpan_number,
                    subsidiarywarshouse_name: $scope.txteditsubsidiarywarshouse_name,
                    warehouse_area: $scope.txteditwarehouse_area, 
                    warehousearea_uomgid: lswarehousearea_uomgid,
                    warehousearea_uom: lswarehousearea_uom,

                    totalcapacity_area: $scope.txtedittotalcapacity_area,
                    totalcapacityarea_uomgid: lsareacapacity_uomgid,
                    area_uom: lsareacapacity_uom, 
                    totalcapacity_volume: $scope.txtedittotalcapacity_volume,
                     
                    volume_uomgid: lsvolume_uomgid,
                    volume_uom: lsvolume_uom,

                    typeofwarehouse_gid: lstypeofwarehouse_gid,
                    typeofwarehouse_name: lstypeofwarehouse_name,

                    creditor_gid: lscreditor_gid,
                    Applicant_name: lsapplicant_name,

                    charges: $scope.txteditcharges,
                    capacity: $scope.txteditcapacity,
                    warehouse_gid: $scope.warehouse_gid,
                    Gstflag: lblGstflag
                }
                var url = 'api/AgrMstWarehouseEdit/UpdatewarehouseDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        if (lspage == "WarehouseEdit") {
                            $state.go('app.AgrMstWarehouseCreationSummary');
                        }
                        else if (lspage == "ProductApproval") {
                            $state.go('app.AgrMstPendingProductApproval');
                        }

                        else if (lspage == "PMGApproval") {
                            $state.go('app.AgrMstPendingPmgApproval');
                        }
                        else if (lspage == "ApprovedWarehouse") {
                            $state.go('app.AgrMstWarehouseAprovalSummary');
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

        $scope.editinstitution_address = function (warehouse2address_gid) {
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
                    warehouse2address_gid: warehouse2address_gid
                }
                var url = 'api/AgrMstWarehouseAdd/EditWarehouseAddressDetail';
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
                        warehouse2address_gid: warehouse2address_gid,
                        warehouse_gid: $location.search().warehouse_gid
                    }
                    var url = 'api/AgrMstWarehouseAdd/UpdateWarehouseAddressDetail';
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

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
            }
        }
        $scope.headoffice_confirm = function (warehouse2branch_gid,warehouse_id) {
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
                        warehouse2branch_gid: warehouse2branch_gid,
                        warehouse_gid: warehouse_id
                    }
                    lockUI();
                    var url = 'api/AgrMstWarehouseEdit/UpdateGSTHeadOffice';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            institutiongstlist();
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

    }

})();
