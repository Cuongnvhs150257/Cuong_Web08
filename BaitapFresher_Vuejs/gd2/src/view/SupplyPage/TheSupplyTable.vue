<template>
  <div class="product-contentner"> 
    <table>
    <thead>
      <tr>
        <th style="min-width: 200px;">MÃ NHÓM VẬT TƯ, HÀNG HÓA, DỊCH VỤ</th>
        <th style="min-width: 500px;">TÊN NHÓM VẬT TƯ, HÀNG HÓA, DỊCH VỤ</th>
        <th style="min-width: 120px;">TRẠNG THÁI</th>
        <th class="product-tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="SupplysLoad">
      <tr
        v-for="su in SupplysLoad.data"
        :key="su.supplyID"
        @dblclick="rowDBClick(su.supplyID)"
      >
        <td>{{ su.supplyCode }}</td>
        <td>{{ su.supplyName }}</td>
        <td>{{ this.fomatSupply(su.status) }}</td>
        <td style="min-width: 110px;">
          <label class="product-tab-th-select-lable" @click="rowDBClick(su.supplyID)">Sửa</label>
          <div class="product-btnopendrop"><MDropItem @edit-value="openPopupAsk" @click="getSupplyDetele(su.supplyID, su.supplyCode)" /></div>
        </td>
      </tr>
    </tbody>
  </table>
  <div class="product-mpopup-ask">
    <MPopupNotification v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteSupply" :getSupplyCode="getsupplydeteteCode" :MPopupN = 2 />
  </div>
  <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>
  </div>
  
</template>

<script>
import MPopupNotification from '../../components/Base/MPopupNotification/MPopupNotification.vue';
import MToast from '../../components/Base/MToast/MToast.vue';
import MDropItem from "../../components/Base/MDropItem/MDropItem.vue";
import configs from '../../configs/index';
import enums from '../../resouce/enums';
import toast from '../../resouce/toast';

export default {
  name: "EmployeeList",
  props: {
    SupplysLoad: Object,
    closeSelectedAll: Boolean
  },
  
  methods: {

    /**
     * Hàm thực hiện kích hoạt toàn bộ checkbox
     * Nguyễn Văn Cương 15/10/2022
     */
    handleCheckBoxAll(){
      //chuyển trạng thái checkbox
      this.stateCheckAll = !this.stateCheckAll;
      if(this.stateCheckAll == true){
          //vòng lặp thêm mã nhân viên vào mảng
          this.SupplysLoad.data.forEach(su => {
            this.listSuSelected.push(su.supplyID);
          },
          this.$emit("get-List-Supply", this.listSuSelected)
          );
      }else{
        this.listSuSelected = [];
      }
      console.log(this.listSuSelected);
    },

    /**
     * hàm thực hiện kích hoạt checkbox
     */
    handleCheckBox(SuID){
          //thêm mã nhân viên đã chọn vào mảng
          this.listWhSelected.push(SuID);
          this.$emit("get-List-Supply", this.listSuSelected)
          console.log(this.listSuSelected);
    },

    /**
     * hàm hiện thông tin trên popup khi nhấn vào Sửa
     * Nguyễn Văn Cương 25/09/2022
     */
    rowDBClick(supplyID) {
      //bấm dbclick để sửa
      this.detailFormMode = 2;
      this.$emit("custom-open-dbclick", supplyID, this.detailFormMode);
      console.log(supplyID);
    },

    /**
     * hàm lấy thông tin nhân viên khi xóa
     * Nguyễn Văn Cương 25/09/2022
     */
    getSupplyDetele(supplyID, supplyCode){
        this.getsupplydetetevalue = supplyID;
        this.getsupplydeteteCode = supplyCode;
        
    },
 
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        console.log(this.checkDelete);
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.idSupplyDelete = this.getsupplydetetevalue; //lưu id employee cần xóa
        }else if(this.checkDelete == 1){
          //bấm nhân bản
           this.detailFormMode = 1;
           this.$emit("custom-open-dbclick", this.getsupplydetetevalue, this.detailFormMode);
        }else{
           console.log(1);
           //ngưng sử dụng
        }
    },

    /**
     * Hàm đóng popup hỏi người dùng có xóa không
     * Nguyễn Văn Cương 25/09/2022
     */
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.popupAskCance = false; //lưu trạng thái đóng popup hỏi người dùng
    },

    /**
     * hàm format giới tính 
     * Nguyễn Văn Cương 01/10/2022
     */
    fomatSupply(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = "Đang sử dụng";
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = "Không sử dụng";
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = "Chưa xác định";
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
     * hàm format ngày tháng 
     * Nguyễn Văn Cương 01/10/2022
     */
    formatDate(date) {
    try {
      
      if (date) {
        var format = "nn/mm/YYYY";
        date = new Date(date);
        // Lấy ra ngày
        let day = date.getDate();
        day = day < 10 ? `0${day}` : day;
        // Lấy ra tháng
        let month = date.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        // Lấy ra năm
        let year = date.getFullYear();
        if(format != "dd/MM/YYYY") return `${day}/${month}/${year}`
        return `${year}-${month}-${day}`;
      } else {
        return "";
      }
    } catch (error) {
      console.log(error);
    }
  },

  /**
    Hàm hiện thị thông báo
    Nguyễn Văn Cương 15/10/2022
     */
    ShowToast(Tstatus){
        this.isShowToast = true; 
        if(Tstatus == true){
          this.Toastcssicon = toast.Toastcssicon_sus;
          this.Toastcss = toast.Toastcss_sus;
          this.ToastMess_color = toast.ToastMess_color_sus;
          this.ToastMess = toast.ToastMessDelete_sus;
        }else{
          this.Toastcssicon = toast.Toastcssicon_faild;
          this.Toastcss = toast.Toastcss_faild;
          this.ToastMess_color = toast.ToastMess_color_faild;
          this.ToastMess = toast.ToastMessDelete_faild;
        }
    },

    /**
     * Hàm xóa employee theo id 
     * Nguyễn Văn Cương 25/09/2022
     */
    async deleteSupply() {
          this.isShowToast = false;
          //lấy employeeid đã lưu 
          var id = this.idSupplyDelete; 
           //check xem người dùng có ấn hủy hay không
          if(this.popupAskCance == true){
          {
            //đóng popup hỏi người dùng
            this.ClosePopupAsk();
              await fetch(
            configs.baseURLSupply + id,
            { method: "DELETE" }
          )
            .then((res) => res.json())
            .then((data) => {
              //load lại data
              this.$emit("data-load-delete");
              this.popupAskCance = true;
              this.ShowToast(this.ToastStatus = true);
              console.log(data);
             
            })
            .catch((res) => {
              this.ShowToast(this.ToastStatus = false);
              console.log(res);
            });
          }
          }
    },
    
  },
  created() {
  },
  data() {
    return {
      supplys: [], //lưu dữ liệu nhân viên
      suSelected: {}, //lưu nhân viên đã chọn
      detailFormMode: 1, //lưu trạng thái mở popup
      isShowAskDelete: false, //gọi popup hỏi có xóa không
      popupAskCance: true, //nút hủy xóa
      idSupplyDelete:0, //lưu id nhân viên cần xóa
      checkDelete: 2,  //trạng thái xóa
      getsupplydetetevalue: 0, //lưu id nhân viên cần xóa
      getsupplydetetecode: '', //lưu code nhân viên cần xóa
      stateCheckAll: false, //lưu trạng thái checkbox
      listSuSelected: [], //lưu danh sách nhân viên cần xóa
      isShowToast: false, //hiển thị thông báo
      ToastStatus: true, //trang thái thông báo
      ToastMess:{}, //nội dung thông báo
      ToastMess_color: {}, //màu nội dung thông báo
      Toastcss:{}, //css thông báo
      Toastcssicon: {}, //icon thông báo
     
    };
  },
  components: {
    MPopupNotification,
    MToast,
    MDropItem
  }
};
</script>

<style scoped>

.product-content-table {
  height: calc(100% - 276px);
  width: calc(100% - 30px);
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
  margin-top: 15px;
}
.product-contentner{
  height: calc(100% - 40px);
  width: 100%;
  overflow: scroll;
  border-radius: 4px;
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
  width: 8px;
  height: 8px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7c869c;
}
.product-content-table table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: unset;
  font-size: 13px;

}

td, th {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 48px;
  font-size: 13px;

}
.product-content-table tr td,
.product-content-table tr th {
  padding-left: 8px;
  text-align: left;
  cursor: pointer;
}.product-content-table tbody tr:hover{
  background-color: rgba(80,184,60,0.1);
}.product-content-table tr:active{
  background-color: #E5F3FF;
}
.product-content-table table thead tr th:first-child{
  min-width: 30px;
}

.product-tab-th-select {
  text-align: center !important;
  padding: 0 !important;
  z-index: 1;
}
.product-content-table thead {
  background-color: rgb(236, 238, 241);
}.product-content-table table thead tr th:last-child{
  min-width: 110px;

  background-color: rgb(236, 238, 241);
  border-left: 1px solid rgb(216, 211, 211);
}
.product-content-table table tbody tr td:last-child{
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
  /*
  position: sticky;
  right: 0; 
  */
  z-index: 1;
  background-color: #fff;

}.product-tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 3px;
  
}.product-tab-th-select-lable{
   margin-top: 16px;
   margin-left: 30px;
   font-weight: 600;
}.product-btnopendrop{
  position: relative;
  width: 5px;
  height: 5px;
  margin-top: 12px;
  margin-left: 30px;

}.product-content-table table thead tr th{
  font-family: Misa Fonts Bold;
  position: sticky;
  top: 0;
  background-color: rgb(236, 238, 241);
  z-index: 3;
  
}
.product-tool-tip-table {
        width: 140px;
        font-size: 11px;
        position: absolute;
        top: 30px;
        right: 0;
        background-color: #505050;
        border-radius: 2px;
        padding: 2px 4px;
        z-index: 3;
        text-align: center;
        color: #fff;
        visibility: hidden;
        
    }
    .product-cmnd:hover .product-tool-tip-table {
        visibility: visible;
        opacity: 1;
    }
</style>

