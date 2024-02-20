(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnWorkItem360', iasnTrnWorkItem360);

        iasnTrnWorkItem360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnTrnWorkItem360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnWorkItem360';
        activate();

        function activate() {
            $scope.PnlSpecific = false;
            $scope.IsVisibleteam=false;
            $scope.IsVisibleemployee=false;
            //$scope.pushback=false;
            //$scope.forward=false;
            $scope.all=false;
            $scope.archival=false;
            $scope.typeE = "";
            $scope.IsLogShow=false;

           $scope.email_gid=localStorage.getItem('email_gid');
           $scope.page = localStorage.getItem('page');
        
            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params={
                lsemail_gid:localStorage.getItem('email_gid')
            };
         
            SocketService.getparams(url,params).then(function (resp) {
               
                $scope.email_from = resp.data.email_from;
                $scope.email_date=resp.data.email_date;
                $scope.email_subject=resp.data.email_subject;
                $scope.email_content=resp.data.email_content;
                $scope.created_date=resp.data.created_date;
                $scope.cc_mail=resp.data.cc;
                $scope.bcc_mail=resp.data.bcc;
                $scope.to_mail=resp.data.email_to;
                $scope.workitemref_no=resp.data.workitemref_no;
                $scope.zone_gid=resp.data.zone_gid;
                $scope.zone_name=resp.data.zone_name;
                $scope.rmemployee_gid=resp.data.rmemployee_gid;
                $scope.rmemployee_name=resp.data.rmemployee_name;   
                $scope.rmemployee_mailid=resp.data.email_address;
                $scope.checkeremployee_name=resp.data.checkeremployee_name;
                $scope.attch_list=resp.data.MdlAttachmentList;
                $scope.allottedby_on=resp.data.allottedby_on;
                $scope.aging=resp.data.aging;
                $scope.status=resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                $scope.Mail_Trigger = resp.data.Mail_Trigger;
                $scope.originalmail_Subject = resp.data.originalmail_Subject;
                $scope.hold_flag = resp.data.hold_flag;
                $scope.workitemhold_reason = resp.data.workitemhold_reason;
                $scope.customer = resp.data.customer_name;
                $scope.rdbcustomer_type = resp.data.customer_type;
                $scope.cutomer_type = resp.data.customer_type;
                $scope.customer_name = resp.data.customer_name;

                if (resp.data.status == 'Pending') {
                    $scope.pendingcustomer = true;
                    $scope.othercustomer = false;
                }
                else {
                    $scope.pendingcustomer = false;
                    $scope.othercustomer = true;
                }
                if (resp.data.customer_type == 'New' || resp.data.customer_type == 'Others') {
                    $scope.customerexisting = false;
                    $scope.customernew = true;
                }
                else if (resp.data.customer_type == 'Existing') {
                    $scope.customerexisting = true;
                    $scope.customernew = false;
                }
                else {
                    $scope.customerexisting = false;
                    $scope.customernew = false;
                }

                if ($scope.archivalremarks==''|| $scope.archivalremarks== null )
                {
                    $scope.archiverem= false;
                }
                else{
                    $scope.archiverem=true; 
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks==''|| $scope.closedremarks== null )
                {
                    $scope.closerem= false;
                }
                else{
                    $scope.closerem= true; 
                }
                $scope.updatedby_on=resp.data.updatedby_on;
                $scope.message_id=resp.data.message_id;
                $scope.reference_id=resp.data.reference_id;
               
                if(resp.data.employee_gid !=null){
                   
                    $scope.assign_to=resp.data.employee_gid;
                   
                }
               

            });
            
         
            var params={
                email_gid:localStorage.getItem("email_gid")
            };
         
            var url="api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                }
                else{

                }
            });

           


            var url = 'api/IasnTrnAuditLog/PostAuditView';

            var params={
                email_gid:localStorage.getItem("email_gid")
            };
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){  
                }
                else{

                }
                
            });

         
        }
      
        $scope.rdbcustomer_new = function (rdbcustomer_type) {
            $scope.customerexisting = false;
            $scope.customernew = true;
            $scope.customer = '';
        }
        $scope.rdbcustomer_existing = function (rdbcustomer_type) {
            $scope.customerexisting = true;
            $scope.customernew = false;
            $scope.customer = '';
        }
        $scope.rdbcustomer_others = function (rdbcustomer_type) {
            $scope.customerexisting = false;
            $scope.customernew = true;
            $scope.customer = '';
        }

        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }

        $scope.UpdateCustomer = function () {
            if ($scope.rdbcustomer_type == '' || $scope.rdbcustomer_type == null || $scope.rdbcustomer_type == undefined) {
                Notify.alert('Kindly Select Customer Type', 'warning');
            }
            else if ($scope.customer == '' || $scope.customer == null || $scope.customer == undefined) {
                Notify.alert('Kindly Enter Customer Name', 'warning');
            }
            else {
                var params = {
                    customer_name: $scope.customer,
                    customer_gid: $scope.customer_gid,
                    customer_type: $scope.rdbcustomer_type,
                    email_gid: $scope.email_gid,
                }
                var url = "api/IasnTrnWorkItem/UpdateCustomer";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();

                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        unlockUI();
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                });
            }
        }

        $scope.back = function () {
            $state.go('app/iasnTrnWorkItemSummary');
        }

         $scope.export = function (path,attchment_name) {
         
           
            var phyPath = path;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = attchment_name.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();

            
}
         $scope.AssignWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name, originalmail_Subject) {
           
    var modalInstance = $modal.open({
        templateUrl: '/assignWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
   
   
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from = email_from;
        $scope.originalmail_Subject = originalmail_Subject;

        var url = "api/IasnMstZone/ZoneSummary";
        SocketService.get(url).then(function (resp) {
       
            $scope.zone_list = resp.data.MdlZoneSummary;
            
        });
        if(zone_gid==undefined || zone_gid==""){
            $scope.zone_flag="N"
           
        }
        else{
            $scope.ddlzone_name=zone_gid;
            $scope.lblzonename=zone_name;
            $scope.zone_flag="Y"
        }
      
        var url = 'api/IasnTrnWorkItem/IsnEmployee';
        SocketService.get(url).then(function (resp) {
            $scope.employee_list = resp.data.MdlIsnEmployee;
           
        });
        $scope.close = function () {
            modalInstance.close('closed');
        };

        $scope.AssignToUpdate=function(){
            
            if($scope.ddlassign_to==undefined){
                modalInstance.close('closed');
                Notify.alert('Kindly Select the Assign to Person','warning');
                return;
            }

            var params={
                email_gid:email_gid,
                employee_gid:$scope.ddlassign_to.employee_gid,
                employee_name:$('#ddlassign_to :selected').text(),
                zone_gid:$scope.ddlzone_name,
                zone_name:$('#ddlzone_name :selected').text(),
                zone_flag: $scope.zone_flag,
                assign_remarks: $scope.assign_remarks,
            }
           
            var url="api/IasnTrnWorkItem/AssignTo";
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }   
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
               
            });
           
        }
    }
}

$scope.logdetails=function(){

    if($scope.IsLogShow==true){
        $scope.IsLogShow=false; 
    }
    else{
        $scope.IsLogShow=true;
        
        var url = 'api/IasnTrnWorkItem/TransferLog';

        var params={
            lsemail_gid:localStorage.getItem("email_gid")
        };
    
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
               
                $scope.transferlog_list = resp.data.MdlTransferLog;
                if( $scope.transferlog_list  == null)
                {
                    $scope.transfershow = true;
                }
                else{
                    $scope.transfershow = false;
                }
            }
            else{
               
            }
            
        });

        var url = 'api/IasnTrnAuditLog/AuditLog';

        var params={
            email_gid:localStorage.getItem("email_gid")
        };            
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
                $scope.auditlog_list = resp.data.MdlAuditLog;
            }
            else{
               
            }
            
        });

       
    }
    
}
$scope.logClose=function(){
    $scope.IsLogShow=false;
}
$scope.TransferWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_gid,zone_name,assign_to) {
   
    var modalInstance = $modal.open({
        templateUrl: '/transferWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
   
   
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
       $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from=email_from;
        $scope.zone_name=zone_name;
        $scope.checkeremployee_name=assign_to;
     
        var url = 'api/IasnTrnWorkItem/IsnEmployee';
        SocketService.get(url).then(function (resp) {
            $scope.employee_list = resp.data.MdlIsnEmployee;
           
        });

        var params={
            lsemail_gid:email_gid
        }
        var url = 'api/IasnTrnWorkItem/TransferLog';
        SocketService.getparams(url,params).then(function (resp) {
            if(resp.data.status==true){
                $scope.transferlog_list = resp.data.MdlTransferLog;
                $scope.showtransfer=true;
            }
            else{
                $scope.showtransfer=false;
            }
           
           
        });
        $scope.close = function () {
            modalInstance.close('closed');
        };

        $scope.transferWIUpdate=function(){
            
            if($scope.transfer_to==undefined){
                modalInstance.close('closed');
                Notify.alert('Kindly Select the Assign to Person','warning');
                return;
            }

            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:$scope.transfer_to,
                employee_name:$('#transfer_to :selected').text(),
                zone_gid:'',
                zone_name:'',
                zone_flag:'Y'
            }
        
            var url="api/IasnTrnWorkItem/AssignTo";
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning');
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }  
                }
            });
           
        }
    }
}

$scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, originalmail_Subject) {
    var modalInstance = $modal.open({
        templateUrl: '/closeWIContent.html',
        controller: ModalInstanceCtrl,
        size: 'md',
        backdrop: 'static',
        keyboard: false,
    });
    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        $scope.workitemref_no=workitemref_no;
        $scope.subject=email_subject;
        $scope.from = email_from;
        $scope.originalmail_Subject = originalmail_Subject;
      
        $scope.CloseWIUpdate=function(){
            
            var params={
                email_gid:localStorage.getItem("email_gid"),
                decision:'Close',
                employee_gid:'',
                employee_name:'',
                remarks:$scope.close_remarks,
                close_acknowledge:$scope.Acknowledge_mail_trigger,
                mailcontent:'Close',
                customer_gid:'',
                customer_name:'',
                subject:'',
                tomail_id:'',
                ccmail_id:'',
                bccmail_id:''
            }
           

            var url='api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'success')
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }                  
                
                }
                else{
                    modalInstance.close('closed');
                    Notify.alert(resp.data.message,'warning')
                    if ($scope.page=='archival')
                    {
                       $state.go("app.iasnTrnArchivalSummary");
                    }
                   else
                   {
                        $state.go("app.iasnTrnWorkItemSummary");
                   }
                }
               
            });
        }

        $scope.close = function () {
            modalInstance.close('closed');
        };
    }
}
$scope.uploadattachment = function (val,val1,name) {
    var item = {
        name: val[0].name,
        file: val[0]
    };
    var frm = new FormData();
    frm.append('fileupload', item.file);
    frm.append('file_name', item.name);
    frm.append('project_flag', "Default");
   
    

    $scope.uploadfrm = frm;
    var url = 'api/IasnTrnWorkItem/MailAttchment';
    lockUI();
    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
        unlockUI();
        $("#addupload").val('');
        $scope.txtdocument_title = '';
        if (resp.data.status == true) {
          
            Notify.alert('Document Uploaded Successfully..!!', 'success')
           
            var url = 'api/IasnTrnWorkItem/MailAttchment';

            SocketService.get(url).then(function (resp) {
               
                $scope.uploaddocument = resp.data.MdlDocDetails;
              
            });
        }
        else {
           
            Notify.alert('File Format Not Supported!')

        }

    });

}

$scope.transferto_change=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to transfer the work item?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Transfer it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#transfer_to :selected').text()
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Transfered Successfully!');
                   
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                  
                }
            });

        }
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}

$scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

$scope.onchangecopy=function(val){
    SweetAlert.swal({
        title: 'Are you sure?',
        text: 'Do you want to move the work item to yours bin?',

        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Yes,Move it!',
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            lockUI();
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:null,
                employee_name:null
            }
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    SweetAlert.swal('Work Item Moved to Your Bin Successfully!');
                   
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    
                }
            });

        }
        else{
            SweetAlert.swal('Error Occured');
        }

    });
}
        $scope.assignto_change=function(val){
            var params={
                email_gid:localStorage.getItem("email_gid"),
                employee_gid:val,
                employee_name:$('#assign_to :selected').text()
            }
            lockUI();
            var url="api/IasnTrnWorkItem/AssignTo";
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status=true){
                    Notify.alert(resp.data.message,'success');
                }
                else{
                    Notify.alert(resp.data.message,'warning');
                }
            });
               
        }
        $scope.updateDesicion=function(){
            var emp_gid;
            var emp_name;
            var customer_gid;
            var customer_name;
            if($scope.txtremarks==undefined){
                Notify.alert('Enter the Remarks','warning');
                return;
            }
            if($scope.decision=='Pushback'){
            
                emp_gid=$scope.rmemployee_gid;
                emp_name=$scope.rmemployee_name;
                
            }

            if($scope.decision=='Forward'){
                if($scope.forward_to==undefined){
                    Notify.alert('Select the forward to person','warning');
                    return;
                }
                emp_gid=$scope.forward_to;
                emp_name=$('#forward_to :selected').text();
            }

            if($scope.decision=='Archival'){
                if($scope.customer==undefined){
                    Notify.alert('Select the Customer','warning');
                    return;
                }
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }

            if($scope.decision=='Close'){
                emp_gid='';
                emp_name='';
                $scope.mailcontent='No Content';
            }
            if($scope.customer==undefined){
                customer_gid='';
                customer_name='';
            }
            else{
                customer_gid=$scope.customer,
                customer_name=$('#customer :selected').text()
            }

            var params={
                email_gid:localStorage.getItem("email_gid"),
                decision:$scope.decision,
                employee_gid:emp_gid,
                employee_name:emp_name,
                remarks:$scope.txtremarks,
                mailcontent:'test',
                customer_gid:customer_gid,
                customer_name:customer_name,
                subject:$scope.pushback_subject,
                tomail_id:$scope.tomail_pushback,
                ccmail_id:$scope.cc_pushback,
                bccmail_id:$scope.bcc_pushback
            }
            var url='api/IasnTrnWorkItem/PostDecision';
            lockUI();
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if(resp.data.status==true){
                    Notify.alert(resp.data.message,'success')

                    var params={
                        lsemail_gid:localStorage.getItem("email_gid")
                    };

                    var url="api/IasnTrnWorkItem/DecisionHistory";
                    SocketService.getparams(url,params).then(function (resp) {
                        if(resp.data.status==true){
                            $scope.decisionHistoryList=resp.data.MdlDecisionhistory;
                        }
                        else{
        
                        }
                    });
                 activate();
                }
                else{
                    Notify.alert(resp.data.message,'warning')
                }
            });


        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/IasnTrnWorkItem/DeleteAttchment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    
                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/IasnTrnWorkItem/MailAttchment';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlDocDetails;

                    });
                }
                else {
                   
                    Notify.alert('Error Occurred')

                }

            });
        }
         $scope.decisionchange=function(val){
              $scope.all=true;
             if(val=="Pushback"){
                $scope.pushback=true;
                $scope.forward=false;
               
                $scope.archival=false;
                $scope.IsVisibleemployee = false;

                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Pushback : "+$scope.email_subject;
                $scope.lsShowPushbackCC=true;

                $scope.tomail_pushback=$scope.rmemployee_mailid;
              
             }
             if(val=="Forward"){
                $scope.pushback=false;
                $scope.forward=true; 
                $scope.archival=false;
                $scope.IsVisibleemployee = true;
                $scope.lsShowPushbackCC=true;
                $scope.tomail_pushback="";
                $scope.cc_pushback=$scope.to_mail+";"+$scope.cc_mail;
                $scope.pushback_subject="Forward : "+$scope.email_subject;
            }
            if(val=="Close"){
                $scope.pushback=false;
                $scope.forward=false; 
                $scope.archival=false;
                $scope.IsVisibleemployee = false;
            }
           
           
            if(val=="Archival"){
                $scope.pushback=false;
                $scope.forward=false;
                $scope.archival=true;
                $scope.IsVisibleemployee = false;
            }
         }

         $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);
           if($scope.page == undefined) 
           {
            localStorage.setItem('page',$scope.page );    
           }   
           else if ($scope.page == 'archival')
           {
            localStorage.setItem('page','archival' );  
           }
           else
           {
             localStorage.setItem('page','workitem' );   
           }
           
            $state.go('app.iasnWomMergeWorkItem');
        }
         $scope.forwardtochange=function(val){
          
            var url="api/IasnTrnWorkItem/EmployeeEmailID";
            var params={
                employee_gid:val
            }
            SocketService.getparams(url,params).then(function (resp) {
                
                    $scope.tomail_pushback=resp.data.employee_emailid;
             
            });
         }
        
         $scope.signature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/signatureContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/IasnTrnEmailSignature/GetEmailSignature';
                SocketService.get(url).then(function (resp) {
                    
                    $scope.EmailSignature = resp.data.emailsignature;
                   
                });
            
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

               
              
                $scope.submit = function () {
                    lockUI();
                    var params={
                        emailsignature:$scope.EmailSignature
                    }
            
                    var url = "api/IasnTrnEmailSignature/PostEmailSignature";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           
                            $modalInstance.close('closed');
                            
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                    });
                }
            }
        }

        $scope.archivalWI = function () {
            var modalInstance = $modal.open({
                templateUrl: '/archivalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
            
                $scope.onclickspecific = function () {
                    $scope.PnlSpecific = true;
                    $scope.customer = "";
                    $scope.cbosanctionrefno = "";
                }
                $scope.onclickcustomer = function () {
                    $scope.PnlSpecific = false;
                    $scope.customer = "";

                }

               $scope.complete=function(string){
                
                    if(string.length >=3){
                     $scope.message="";
                     var url = 'api/customer/ExploreCustomer';
                     var params={
                          customername:string 
                      }
                      SocketService.getparams(url,params).then(function (resp) {
                          if(resp.data.status==true){
                             $scope.message="";
                             $scope.customer_list = resp.data.Customers;
                          }
                          else{
                              $scope.customer="";
                             $scope.message="No Records";
                          }
                         
                         
                  });
            }
            else{
             $scope.customer_list=null;
                $scope.message="Type atleast three character";
            }
                 }
        
               $scope.fillTextbox=function(customer_gid,customer_name){
           
               $scope.customer=customer_name;
               $scope. customer_gid=customer_gid;
               $scope.customer_list=null;

               var params = {
                customer_gid: customer_gid
            }

           
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.sanctiondtl = resp.data.sanctiondtl;
               
            });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.ArchivalSubmit=function(){
                    var WorkItem_List = [];
                    var email_gid;
                    var sanctionref_no='';
                    var sanction_gid='';
                  
                    email_gid=localStorage.getItem("email_gid")
                    WorkItem_List.push(email_gid);
                    if($scope.archival.types_of_archival=='Specific'){
                      if($scope.cbosanctionrefno == undefined){
                        modalInstance.close('closed');
                        Notify.alert('Select the Sanction Ref No.','warning');
                        return;
                      }
                      else{
                        sanctionref_no=$('#sanction :selected').text();
                        sanction_gid=$scope.cbosanctionrefno.sanction_Gid;
                      }
                    }
                    
                   
                    var params={
                        email_gid: WorkItem_List,
                        archival_type:$scope.archival.types_of_archival,
                        remarks: $scope.archival.Remarks,
                        customer_gid:$scope.customer_gid,
                        customer_name:$scope.customer,
                        sanctionref_no:sanctionref_no,
                        sanction_gid:sanction_gid
                    }
                
                        var url = 'api/IasnTrnWorkItem/PostArchival';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {                              
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')  
                                if ($scope.page=='archival')
                                {
                                   $state.go("app.iasnTrnArchivalSummary");
                                }
                               else
                               {
                                    $state.go("app.iasnTrnWorkItemSummary");
                               }
                            }
                            else {
                                
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message)
                                if ($scope.page=='archival')
                                {
                                   $state.go("app.iasnTrnArchivalSummary");
                                }
                               else
                               {
                                    $state.go("app.iasnTrnWorkItemSummary");
                               }
                            }                       
                      
                        });
                   
                }
            }
        }

        $scope.pushback=function(){
            $scope.ccMail=$scope.cc_mail;
           localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
           localStorage.setItem('toMail',$scope.rmemployee_mailid);
           localStorage.setItem('ccMail', $scope.ccMail);
           localStorage.setItem('bccMail', $scope.bcc_mail);
           localStorage.setItem('email_subject',$scope.email_subject);
           localStorage.setItem('message_id',$scope.message_id);
           localStorage.setItem('reference_id',$scope.reference_id);
           localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
           localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
           localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
           localStorage.setItem('decision', 'Pushback');
           localStorage.setItem('lspage', 'workitem');
           $state.go('app.composeMail');
         
       }
       
       $scope.forward=function(){
           $scope.ccMail=$scope.cc_mail;
          localStorage.setItem('email_gid',localStorage.getItem("email_gid"));
          localStorage.setItem('toMail',$scope.rmemployee_mailid);
          localStorage.setItem('ccMail', $scope.ccMail);
          localStorage.setItem('bccMail', $scope.bcc_mail);
          localStorage.setItem('email_subject',$scope.email_subject);
          localStorage.setItem('message_id',$scope.message_id);
          localStorage.setItem('reference_id',$scope.reference_id);
          localStorage.setItem('rmemployee_gid', $scope.rmemployee_gid);
          localStorage.setItem('rmemployee_name', $scope.rmemployee_name);
          localStorage.setItem('originalmail_Subject', $scope.originalmail_Subject);
           localStorage.setItem('decision', 'Forward');
           localStorage.setItem('lspage', 'workitem');
           $state.go('app.composeMail');
       
        
       }
       $scope.back = function () {
           if ($scope.page == 'Workitem')
           {
               $state.go("app.iasnTrnWorkItemSummary");
           }
           else if($scope.page == 'Allotted')
           {
               $state.go("app.iasnTrnAllotedSummary");
           }
           else if ($scope.page == 'Pushback')
           {
               $state.go("app.iasnTrnPushbackSummary");
           }
           else if ($scope.page == 'Forward')
           {
               $state.go("app.iasnTrnForwardSummary");
           }
           else if ($scope.page == 'Close')
           {
               $state.go("app.iasnTrnCloseSummary");
           }
           else if ($scope.page == 'Archival')
           {
               $state.go("app.iasnTrnArchivalSummary");
           }
         }

       $scope.forwardNew_Mail = function (val, val1, val2, val3, val4) {
           $scope.ccMail = $scope.cc_mail;
           localStorage.setItem('composemail_gid', val);
           localStorage.setItem('toMail', val2);
           localStorage.setItem('ccMail', val3);
           localStorage.setItem('bccMail', val4);
           localStorage.setItem('email_subject', val1);
           localStorage.setItem('decision', 'Forward');
           localStorage.setItem('lspage', 'Archival');
           $state.go('app.iasnTrnForwardMail');
       }

       $scope.archivalNew_Mail = function (val) {
           var modalInstance = $modal.open({
               templateUrl: '/archivalContent.html',
               controller: ModalInstanceCtrl,
               size: 'md',
               backdrop: 'static',
               keyboard: false,
           });
           ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
           function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

               $scope.onclickspecific = function () {
                   $scope.PnlSpecific = true;
                   $scope.customer = "";
                   $scope.cbosanctionrefno = "";
               }
               $scope.onclickcustomer = function () {
                   $scope.PnlSpecific = false;
                   $scope.customer = "";

               }

               $scope.complete = function (string) {

                   if (string.length >= 3) {
                       $scope.message = "";
                       var url = 'api/customer/ExploreCustomer';
                       var params = {
                           customername: string
                       }
                       SocketService.getparams(url, params).then(function (resp) {
                           if (resp.data.status == true) {
                               $scope.message = "";
                               $scope.customer_list = resp.data.Customers;
                           }
                           else {
                               $scope.customer = "";
                               $scope.message = "No Records";
                           }


                       });
                   }
                   else {
                       $scope.customer_list = null;
                       $scope.message = "Type atleast three character";
                   }
               }

               $scope.fillTextbox = function (customer_gid, customer_name) {

                   $scope.customer = customer_name;
                   $scope.customer_gid = customer_gid;
                   $scope.customer_list = null;

                   var params = {
                       customer_gid: customer_gid
                   }


                   var url = 'api/loan/customer_getheads';

                   SocketService.getparams(url, params).then(function (resp) {

                       $scope.sanctiondtl = resp.data.sanctiondtl;

                   });
               }

               $scope.close = function () {
                   modalInstance.close('closed');
               };

               $scope.ArchivalSubmit = function () {
                   var sanctionref_no = '';
                   var sanction_gid = '';

                   if ($scope.archival.types_of_archival == 'Specific') {
                       if ($scope.cbosanctionrefno == undefined) {
                           modalInstance.close('closed');
                           Notify.alert('Select the Sanction Ref No.', 'warning');
                           return;
                       }
                       else {
                           sanctionref_no = $('#sanction :selected').text();
                           sanction_gid = $scope.cbosanctionrefno.sanction_Gid;
                       }
                   }


                   var params = {
                       composemail_gid: val,
                       archival_type: $scope.archival.types_of_archival,
                       remarks: $scope.archival.Remarks,
                       customer_gid: $scope.customer_gid,
                       customer_name: $scope.customer,
                       sanctionref_no: sanctionref_no,
                       sanction_gid: sanction_gid,
                       status: "Archival"
                   }
                   var url = 'api/IasnTrnWorkItem/ComposeMailDecision';
                   lockUI()
                   SocketService.post(url, params).then(function (resp) {
                       unlockUI();
                       if (resp.data.status == true) {
                           modalInstance.close('closed');
                           Notify.alert(resp.data.message, 'success')

                       }
                       else {

                           modalInstance.close('closed');
                           Notify.alert(resp.data.message)
                       }
                       activate();
                       if ($scope.page == 'Archival') {
                           $state.go("app.iasnTrnArchivalSummary");
                       }
                       else {
                           $state.go("app.iasnWomWorkOrderSummary");
                       }
                   });

               }
           }
       }
    }
})();
