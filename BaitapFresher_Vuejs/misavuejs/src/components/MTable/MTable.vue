<template>
    <table>
                    <thead>
                        <tr>
                            <th><input type="checkbox"></th>
                            <th >Mã nhân viên</th>
                            <th>Tên nhân viên</th>
                            <th>Giới tính</th>
                            <th class="tab-th-select" >Ngày sinh</th>
                            <th>Số CMND</th>
                            <th>Chức danh</th>
                            <th>Tên đơn vị</th>
                            <th>Số tài khoản</th>
                            <th>Tên ngân hàng</th>
                            <th>Chi nhánh ngân hàng</th>
                            <th class="tab-th-select">Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="emp in employees" :key="emp.EmployeeID">
                            <td><input type="checkbox"></td>
                            <td>{{emp.EmployeeCode}}</td>
                            <td>{{emp.FullName}}</td>
                            <td>{{emp.Gender}}</td>
                            <td class="tab-th-select">{{emp.DateOfBirth}}</td>
                            <td>{{emp.IdentityNumber}}</td>
                            <td>{{emp.PositionName}}</td>
                            <td>{{emp.DepartmentName}}</td>
                            <td>{{emp.AccountBank}}</td>
                            <td>{{emp.NameBank}}</td>
                            <td>{{emp.BranchBank}}</td>
                            <td class="tab-th-select">Sửa
                                <select>
                                   <option value="1">Nhân bản</option>
                                   <option value="2">Xóa</option>
                                   <option value="3">Ngưng sử dụng</option>
                                </select>
                            </td>
                        </tr>

                        
                    </tbody>
                </table>
</template>

<script>
export default {
    name: "EmployeeList",
    created(){
        fetch("https://63215c8cfd698dfa29f620da.mockapi.io/Employees", {method:"GET"})
        .then(res => res.json())
        .then(data =>{
            this.employees = data;
            console.log(data);
        })
        .catch(res =>{
            console.log(res);
        })
    },
    data(){
        return{
            employees:[]
        }
    },
}
</script>

<style>
    .content-table{
    height: calc(100% - 115px);
    width: calc(100% - 30px);
    background-color: #fff;
    border: 10px solid #fff;
    padding-left: 10px;
    overflow: scroll;

}
::-webkit-scrollbar {
  height: 8px;
  width: 8px;
  
}
::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
  
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
  cursor: pointer;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7C869C;
}
.content-table table{
    width: 100%;
    border-collapse: collapse;
    border-spacing: unset;
    font-size: 13px;
    
}

.content-table tr, .content-table th, .content-table td{
    border-bottom: 1px solid rgb(216, 211, 211);
    border-right: 1px solid rgb(216, 211, 211);
    height: 35px;
   
}
.content-table tr td, .content-table tr th{
    padding-left: 5px;
    text-align: left;
}
.tab-th-select{
    text-align: center !important;
}
.content-table thead{
    background-color: rgb(236,238,241);
}.tab-th-select select{
    width: 20px;
    height: 15px;
    border: none;
    outline: none;
}

</style>

