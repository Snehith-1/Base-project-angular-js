(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnWomMergeWorkItem', iasnWomMergeWorkItem);

        iasnWomMergeWorkItem.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnWomMergeWorkItem($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnWomMergeWorkItem';
        var lsemail_gid;
        activate();
        $scope.UnTaggedWI_List=undefined;
        $scope.ShowSearch=false;

        function activate() {
            $scope.page = localStorage.getItem('page')
            lsemail_gid=localStorage.getItem('email_gid')
            $scope.email_subject=localStorage.getItem('email_subject')
            $scope.workitemref_no=localStorage.getItem('workitemref_no');
         
            var params={
                email_gid:lsemail_gid
            }
            var url = 'api/IasnMergeWorkItem/TaggedWISummary';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.TaggedWI_List = resp.data.MdlWorkItem;
                
                if($scope.TaggedWI_List!=null){
                    $scope.count_workitem=$scope.TaggedWI_List.length;
                    $scope.Tagtotal=$scope.TaggedWI_List.length;
                }

            });
        }

        $scope.complete_searchrm=function(string){
                
            if(string.length >=3){
             $scope.rmmessage="";
             var url = 'api/IasnMergeWorkItem/WIEmailFromList';
             var params={
                email_from:string 
              }
              SocketService.getparams(url,params).then(function (resp) {
                  if(resp.data.status==true){
                     $scope.rmmessage="";
                     $scope.rm_list = resp.data.MdlWISearch;
                  }
                  else{
                      $scope.searchrm_name="";
                     $scope.rmmessage="No Records";
                  }
                 
                 
          });
    }
    else{
     $scope.rm_list=null;
        $scope.rmmessage="Type atleast three character";
    }
           
        
         }
         $scope.fillTextboxRM=function(val){
         
             $scope.searchrm_name=val;
           
             $scope.rm_list=null;

    }


    $scope.complete_searchsubject=function(string){
                
        if(string.length >=3){
         $scope.subjectmessage="";
         var url = 'api/IasnMergeWorkItem/WISubjectList';
         var params={
            email_subject:string 
          }
          SocketService.getparams(url,params).then(function (resp) {
              if(resp.data.status==true){
                 $scope.subjectmessage="";
                 $scope.subject_list = resp.data.MdlWISearch;
              }
              else{
                $scope.searchsubject="";
                 $scope.subjectmessage="No Records";
              }
             
             
      });
}
else{
 $scope.subject_list=null;
    $scope.subjectmessage="Type atleast three character";
}
       
    
     }
     $scope.fillTextboxSubject=function(val){
     
         $scope.searchsubject=val;
       
         $scope.subject_list=null;

}

$scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
$scope.SearchWorkItem=function(){
    lockUI();
    $scope.ShowSearch=true;
    if($scope.searchrm_name==undefined){
        $scope.searchrm_name=''
    }

    if($scope.searchsubject==undefined){
        $scope.searchsubject=''
    }

    if($scope.searchzone_name==undefined){
        $scope.searchzone_name=''
    }

    var params={
        email_gid:localStorage.getItem('email_gid'),
        email_from:$scope.searchrm_name,
        email_subject:$scope.searchsubject,
        zone_name:$scope.searchzone_name
    }

    var url = 'api/IasnMergeWorkItem/UnTaggedWISummary';
    SocketService.post(url,params).then(function (resp) {
        $scope.UnTaggedWI_List = resp.data.MdlWorkItem;
        
        if($scope.UnTaggedWI_List!=null){
            $scope.count_UnTagworkitem=$scope.UnTaggedWI_List.length;
            $scope.UnTagTotal=$scope.UnTaggedWI_List.length;
        }

    });

    unlockUI();
}

$scope.MergeWorkItem=function(){
   
    if($scope.UnTaggedWI_List==undefined){
        Notify.alert('Search the Work Item')
       
        return;
    }
    var emailgid_list=[];
    var tocheck_data;
    angular.forEach($scope.UnTaggedWI_List, function (val) {

        if (val.checked == true) {
            tocheck_data=val.email_gid;
            emailgid_list.push(val.email_gid);

           
        }
    });
   
    if (tocheck_data ==undefined)
        {
        
        Notify.alert('Select Atleast One Record!')
       
        return;
    }

    var params={
        mergeemail_gid:emailgid_list,
        email_gid:localStorage.getItem('email_gid')
    }

    var url = 'api/IasnMergeWorkItem/WIMerge';
    lockUI();
    SocketService.post(url,params).then(function (resp) {
        $scope.UnTaggedWI_List=null;
        unlockUI();
        activate();
        if(resp.data.status==true){
            $scope.searchrm_name='';
            $scope.searchsubject='';
            $scope.searchzone_name='';
            Notify.alert(resp.data.message,'success')
        }
        else{
            Notify.alert(resp.data.message,'warning')
        }
    });

   
    
}

$scope.complete_searchzone=function(string){
                
    if(string.length >=3){
     $scope.zonemessage="";
     var url = 'api/IasnMergeWorkItem/WIZoneList';
     var params={
        zone_name:string 
      }
      SocketService.getparams(url,params).then(function (resp) {
          if(resp.data.status==true){
             $scope.zonemessage="";
             $scope.zone_list = resp.data.MdlWISearch;
          }
          else{
            $scope.searchzone_name="";
             $scope.zonemessage="No Records";
          }
         
         
  });
}
else{
$scope.zone_list=null;
$scope.zonemessage="Type atleast three character";
}
   

 }

 $scope.checkall = function (selected) {
    angular.forEach($scope.UnTaggedWI_List, function (val) {  
        
            val.checked = selected;
    });
}

$scope.back=function(){
    if($scope.page =="workitem"){
        $state.go("app.iasnTrnWorkItem360");

    }
    else if($scope.page =="workitemsummary"){
        localStorage.setItem = ('page','workitemsummary')
        $state.go("app.iasnTrnWorkItem360");     

    }
    else if($scope.page =="myworkitem")
    {
        $state.go("app.iasnTrnMyWorkItem360");

    }
    else if($scope.page =="MyWorkItemSummary"){
        localStorage.setItem = ('page','MyWorkItemSummary')
        $state.go("app.iasnTrnMyWorkItem360");
    }
    else if ($scope.page == "archival"){
        localStorage.setItem = ('page','archival')
        $state.go("app.iasnTrnWorkItem360");
    }
    else if($scope.page =="workitemsummarypage")
    {
        $state.go("app.iasnWomWorkOrderSummary");

    }
    else if($scope.page =="MyWorkItemSummarypage")
    {
        $state.go("app.iasnTrnMyWorkItemSummary");

    }
    
}

 $scope.fillTextboxZone=function(val){
 
     $scope.searchzone_name=val;
   
     $scope.zone_list=null;

}

$scope.EmployeeProfile=function(emp_gid){
    var url = 'api/IasnTrnWorkItem/EmployeeProfile';
    var params={
        employee_gid:emp_gid
    }
    SocketService.getparams(url,params).then(function (resp) {
       if(resp.data.status==true){
        $scope.user_code=resp.data.user_code;
        $scope.user_name=resp.data.user_name;
        $scope.user_photo=resp.data.user_photo;
        $scope.user_designation=resp.data.user_designation;
        $scope.user_department=resp.data.user_department;
        $scope.user_mobileno=resp.data.user_mobileno;
       }
       else{
        $scope.user_code="-";
        $scope.user_name="-";
        $scope.user_photo="N";
        $scope.user_designation="-";
        $scope.user_department="-";
       }
    });

}


        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val)
            $state.go("app.iasnTrnWorkItem360");
        }

        $scope.UndoMail = function (val) {
            var params = {
                email_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Undo the Mail Merge ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Undo it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IasnMergeWorkItem/WIUndoMerge";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Reverted!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
       
    }
})();
