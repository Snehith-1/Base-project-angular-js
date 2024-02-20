(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasCourierEdit', idasCourierEdit);

    idasCourierEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService','cmnfunctionService'];

    function idasCourierEdit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService,cmnfunctionService) {
       
        $scope.title = 'idasCourierEdit';
        var vm = this;
        vm.title = 'idasTrnCourierCreation';
        var courier_gid;
        var sanction_gid;
        var page;
        activate();

        function activate() {

            courier_gid=localStorage.getItem('courier_gid'); 
            page=localStorage.getItem('page');  
                    // Calender Popup... //
            $scope.courier_gid=courier_gid;
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };


            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ["dd-MM-yyyy"];
            vm.format = vm.formats[0];

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
                
            });

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var  params={
                courier_gid:courier_gid
            }

            var url = 'api/IdasMstCourierCompany/CourierCompanySummary';
            SocketService.get(url).then(function (resp) {
                $scope.couriercompany_list = resp.data.MdlCourierCompany;
            });

            var url = 'api/IdasCourierManagement/GetEditCourierDetail';
            SocketService.getparams(url,params).then(function (resp) {
                console.log(resp.data);
                if (resp.data.status == true) {
                    if (resp.data.customer_gid == "")
                    {
                        $scope.rdbcustomeredit = "No";
                        $scope.customerno = true;
                        $scope.customernameedit = resp.data.customer_name;
                    }
                    else {
                        $scope.rdbcustomeredit = "Yes";
                        $scope.customeryes = true;
                        $scope.customername = resp.data.customer_gid;
                    }

                    $scope.date_of_courier = resp.data.date_of_courier;
                    $scope.date_of_courier = Date.parse($scope.date_of_courier);

                    //$scope.date_of_courier = Date.Parse(resp.data.date_of_courier);
                    console.log($scope.date_of_courier);
                    $scope.courierref_no=resp.data.courierref_no;
                   //$scope.cbocustomer2sanction_gid=resp.data.sanction_gid;
                    $scope.document_type=resp.data.document_type;
                    $scope.address=resp.data.address;
                   // $scope.sender_name=resp.data.sender_gid;
                    $scope.pod_no=resp.data.pod_no;
                    $scope.courier_company_name=resp.data.couriercompany_name;
                  //  $scope.courierhandover_to=resp.data.courierhandover_to;
                   // $scope.courierhandover_to_gid=resp.data.courierhandover_to_gid;
                    $scope.courier_type=resp.data.courier_type;
                    $scope.remarks=resp.data.remarks;
                    $scope.courier_sender_name=resp.data.sender_gid;
                    $scope.handover_name=resp.data.courierhandover_to_gid;
                    $scope.uploadDoc_list=resp.data.uploadcourierdocument;
                    sanction_gid=resp.data.sanction_gid;
                    $scope.ack_status=resp.data.ack_status;
                    $scope.employee_list=resp.data.MdlEmployee;

                    if (resp.data.MdlCourierByList != null) {
                        $scope.sender_name = [];
                        var count = resp.data.MdlCourierByList.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCourierByList[i].employee_gid);
                            $scope.sender_name.push($scope.employee_list[indexs]);
                        }
                    }
    
                    if (resp.data.MdlCourierToList != null) {
                        var count = resp.data.MdlCourierToList.length;
                        $scope.handover_name = [];
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCourierToList[i].employee_gid);
                            $scope.handover_name.push($scope.employee_list[indexs]);
                        }
                    }

                    var params = {
                        customer_gid: resp.data.customer_gid
                    }
                    var url = 'api/loan/customer_getheads';
    
                    SocketService.getparams(url, params).then(function (resp) {
                    
                        $scope.sanctiondtl = resp.data.sanctiondtl;
                        console.log('sanctiondtl',$scope.sanctiondtl);
                    });
                    $scope.cbosanctionGid =sanction_gid;

                }
                
                if($scope.courier_type=="Courier Outward" || $scope.courier_type=="Courier Inward"){
                    document.getElementById("courier_company_name").disabled = false;
                    document.getElementById("pod_no").disabled=false;
                    $scope.physical_value=false;
                }
                if($scope.courier_type=="Courier Outward"){
                    $scope.courier_outward=true;
                    $scope.courier_inward=false;
                    $scope.physical_inward=false;
                    $scope.physical_outward=false;
                
                
                }
    
                if($scope.courier_type=="Courier Inward"){
                    //$scope.courier_inward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=true;
                    $scope.physical_inward=false;
                    $scope.physical_outward=false;
                
                }
    
                if($scope.courier_type=="Physical Inward"){
                  //  $scope.physical_inward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=false;
                    $scope.physical_inward=true;
                    $scope.physical_outward=false;
                
                }
                if($scope.courier_type=="Physical Outward"){
                  //  $scope.physical_outward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=false;
                    $scope.physical_inward=false;
                    $scope.physical_outward=true;
                
                }
                if($scope.courier_type=="Physical Inward" || $scope.courier_type=="Physical Outward"){
                    document.getElementById("courier_company_name").disabled = true;
                    document.getElementById("pod_no").disabled=true;
                    $scope.physical_value=true;
                }
    
            });

            $scope.rdbcustomer_yes = function () {
                $scope.customerno = false;
                $scope.customeryes = true;
            }
            $scope.rdbcustomer_no = function () {
                $scope.customerno = true;
                $scope.customeryes = false;
            }

        }
       
        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: customer
            }
            console.log('customer params',params);
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                console.log('sanctionDtls',resp.data);
              if(resp.data.sanctiondtl!=null){
                $scope.sanctiondtl = resp.data.sanctiondtl;
              }
              else{
                $scope.sanctiondtl =null;
              }
                
             
            });
        }
       
        $scope.downloadsdocument = function (val1, val2) {

            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.commondocumentupload = function (val, val1, name) {
           
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_gid', $scope.document_gid);
            frm.append('Trn_Gid',courier_gid);
            frm.append('document_title',$scope.txtdocument_title);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasCourierManagement/CourierDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {


                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                  activate();
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }
         $scope.courierback = function () {
            $location.url('app/idasCourierMgmtsummary?lstab='+page);
        }

        $scope.update=function(){
var courier_company_name;
            if($scope.ack_status=='Acknowledged'){
                console.log($scope.ack_date);
                if($scope.ack_date==undefined||$scope.ack_date==""){
                    Notify.alert('Select the Acknowledgement Date');
                    return;
                }
            }

            if($scope.courier_type=="Courier Inward" || $scope.courier_type=="Courier Outward"){
                if($scope.courier_company_name==undefined ||$scope.courier_company_name=="" ){
                    Notify.alert('Select the Courier Company');
                    return;
                }
                else{
                    courier_company_name=$('#courier_company_name :selected').text()
                }

                if($scope.pod_no==undefined||$scope.pod_no==""){
                    Notify.alert('Enter the POD No.');
                    return;
                }
            }
            else{
              courier_company_name="";
                $scope.pod_no="";
            }
            if($scope.cbosanctionGid==(undefined || "")){
                $scope.sanctionref_no='';
                $scope.sanction_gid='';
            }
            else{
                $scope.sanctionref_no= $('#cbosanctionGid :selected').text();
                $scope.sanction_gid= $scope.cbosanctionGid;
            }
            if ($scope.rdbcustomeredit == "No") {
                var customername = $scope.customernameedit;
                $scope.customername = '';
            }
            else {
                var customername = $('#customername :selected').text();
            }
            var params={
                date_of_courier:$scope.date_of_courier,
                document_type:$scope.document_type,
                remarks:$scope.remarks,
                pod_no:$scope.pod_no,
                couriercompany_name:courier_company_name,
                address:$scope.address,
                customer_gid:$scope.customername,
                customer_name:customername,
                sanction_gid:$scope.sanction_gid,
                sanctionref_no:$scope.sanctionref_no,
                courierMgmt_gid :courier_gid,
                courier_type:$scope.courier_type,
                ack_status:$scope.ack_status,
                ack_date:$scope.ack_date,
                MdlCourierByList: $scope.sender_name,  
                MdlCourierToList: $scope.handover_name,
            }
            lockUI();

            var url = 'api/IdasCourierManagement/PostUpdateCourier';
            SocketService.post(url, params).then(function (resp) {
unlockUI();
                if (resp.data.status == true) {
                   
                    $location.url('app/idasCourierMgmtsummary?lstab='+page);
                    Notify.alert(resp.data.message, 'success')
                }

                else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }
    }
})();
