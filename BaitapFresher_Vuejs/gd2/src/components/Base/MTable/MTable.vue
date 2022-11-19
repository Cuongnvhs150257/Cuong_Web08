<template>
  <div class="contentner"  id="opendrop" > 
    <table >
    <thead>
      <tr>
        <th v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBoxAll" :stateCheckAll="stateCheckAll"  /></th>
        <th v-for="index in thListTable" :key="index.label" :class="index.class" :style="index.style">{{index.label}} <div @click="openFilter(index.inputfilter, index.property, index.filterlabel)" class="filter-header-icon"></div> <span class="tooltip" v-if="index.span">{{index.span}}</span></th>
        <th class="tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="RecordsLoad" >
      <tr
        v-for="re in RecordsLoad.data"
        :key="re[tdListTable[tdListTable.length - 1].property]"
        @dblclick="rowDBClick(re[tdListTable[tdListTable.length - 1].property])"
      >
        <td v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBox(re[tdListTable[tdListTable.length - 1].property])"  :stateCheckAll="stateCheckAll" /></td>
        <td v-for="index in tdListTable" :class="index.class" :style="index.style" :key="index" >{{index.fun === 1 ? this.formatStatusRecord(re[index.property]) 
          : index.fun === 2 ? this.formatGenderRecord(re[index.property])
          : index.fun === 3 ? this.formatDateRecord(re[index.property])
          : index.fun === 4 ? this.formatTaxRecord(re[index.property])
          : index.fun === 5 ? this.formatNatureRecord(re[index.property])
          : re[index.property] }}
          </td>
        <td style="min-width: 110px;">
          
          <label class="tab-th-select-lable" @click="rowDBClick(re[tdListTable[tdListTable.length - 1].property])">Sửa</label>
          <div class="btnopendrop" @click="getRecordDetele(re[tdListTable[tdListTable.length - 1].property], re[tdListTable[1].property])" ><MDropItem @edit-value="openPopupAsk" :MDropSta="1"  :Drstyle="StyleDropbox" @getpostion="getPostion" /></div>
        </td>
      </tr>
    </tbody>
    <tbody v-if="RecordsMuti">
        <tr v-for="re in RecordsMuti" :key="re" :style="re.style" @dblclick="rowDBClick(re.arr[tdListTable[tdListTable.length - 1].property])">
          <td v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBox(re.arr[tdListTable[tdListTable.length - 1].property])"  :stateCheckAll="stateCheckAll" /></td>
          <td v-for="index in tdListTable" :style="index.style" :key="index" >
            <div class="mutitable" >
                <div @click="openChild(re, RecordsMuti)" :class="{'mutitable-icon' : !Changeicon && !re.class && re.style, 'mutitable-icon b' : Changeicon && !re.class && re.style}" v-if="index.muti === 1"></div>
                <div :class="{'mutitable-label': re.class && index.muti == 1, 'mutitable-label b': !re.class && index.muti == 1, 'mutitable-label c': !re.class && re.style && index.muti == 1}">
                  {{index.fun === 1 ? this.formatStatusRecord(re.arr[index.property]) 
                  : index.fun === 2 ? this.formatGenderRecord(re.arr[index.property])
                  : index.fun === 3 ? this.formatDateRecord(re.arr[index.property])
                  : index.fun === 4 ? this.formatTaxRecord(re.arr[index.property])
                  : index.fun === 5 ? this.formatNatureRecord(re.arr[index.property])
                  : re.arr[index.property]}}
                </div>
            </div>
          </td>
          <td style="min-width: 110px;">
            <label class="tab-th-select-lable" @click="rowDBClick(re.arr[tdListTable[tdListTable.length - 1].property])">Sửa</label>
            <div class="btnopendrop" @click="getRecordDetele(re.arr[tdListTable[tdListTable.length - 1].property], re.arr[tdListTable[1].property])"><MDropItem @edit-value="openPopupAsk" :MDropSta="1" :Drstyle="StyleDropbox" @getpostion="getPostion" /></div>
        </td>
        </tr>
    </tbody>
    <tfoot v-if="tfoot">
        <td></td>
        <td>Tổng</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td>0,00</td>
        <td>0,0</td>
        <td></td>
        <td></td>
    </tfoot>
  </table>
  <div class="mpopup-ask">
    <MPopupNotification v-if="isShowAskDelete" :record="PopupNotilable" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteRecord" @close-notification-click="ClosePopupAsk"
    :getRecordCode="getrecorddeteteCode" :MPopupN="PopupNotificationMode" />
    
  </div>
  <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>
  </div>
  
</template>

<script>
import MPopupNotification from '../MPopupNotification/MPopupNotification.vue';
import MCheckbox from "../MCheckbox/MCheckbox.vue";
import MToast from '../MToast/MToast.vue';
import MDropItem from "../MDropItem/MDropItem.vue";
import configs from '../../../configs/index';
import enums from '../../../resouce/enums';
import toast from '../../../resouce/toast';

export default {
  name: "RecordList",
  props: {
    RecordsLoad: Object,
    closeSelectedAll: Boolean,
    thListTable: [],
    tdListTable: [],
    tfoot: Boolean,
    PopupNotilable: String,
    baseURL: String,
    TableCheckBox: Boolean,
    RecordsMuti: Object,
    CheckDeleteIns: Boolean,
  },
  components: {
    MPopupNotification,
    MCheckbox,
    MToast,
    MDropItem,
  },
  mounted() {
    window.addEventListener('mousedown', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
  
  methods: {
    /**
    Hàm mở td con của table đệ quy
    Nguyễn Văn Cương 10/11/2022
     */
    openChild(value, re){
      //tìm giá trị trong mảng
      const index = this.RecordsMuti.indexOf(value);
      //đóng mở td con
      this.openChildMuti = !this.openChildMuti;
      //lưu style hiển thị
      const style = "none";
      const style1 = "display: none";
      if(this.openChildMuti){
        re[index + 1].style = style;
        this.Changeicon = true;
      }else{
        re[index+ 1].style = style1;
        this.Changeicon = false;
      }
      
    },

    /**
         * hàm click outsite
         * Nguyễn Văn Cương 01/10/2022
         */
        clickEventInterrupt(event){
          //lưu vị trí con chuột left, top
          this.PosY = event.y;
          this.PosX = event.x
        },

    openFilter(inputfil,value, filterlabel){
      this.$emit("Show-Filter", 2,inputfil, value, filterlabel, this.PosY, this.PosX);
    },

    /**
    Hàm lấy vị trí để hiển thị dropbox
    Nguyễn Văn Cương 10/11/2022
     */
    getPostion(DropboxLeft, DropboxTop){
      //vị trí cách trái
      DropboxLeft = DropboxLeft - 300;
      //vị trí cách trên
      DropboxTop = DropboxTop - 36;
        this.StyleDropbox = "left: " + DropboxLeft + "px;" + "top: " + DropboxTop + "px;";
    },

    /**
     * Hàm thực hiện kích hoạt toàn bộ checkbox
     * Nguyễn Văn Cương 15/10/2022
     */
    handleCheckBoxAll(){
      //chuyển trạng thái checkbox
      this.stateCheckAll = !this.stateCheckAll;
      if(this.stateCheckAll == true){
          //vòng lặp thêm mã nhân viên vào mảng
          this.RecordsLoad.data.forEach(re => {
            this.listReSelected.push(re[this.tdListTable[this.tdListTable.length - 1].property]);
          },
          this.$emit("get-List-Checkbox", this.listReSelected)
          );
      }else{
        this.listReSelected = [];
      }
    },

    /**
     * hàm thực hiện kích hoạt checkbox
     */
    handleCheckBox(ReID){
          //thêm mã nhân viên đã chọn vào mảng
          this.listReSelected.push(ReID);
          this.$emit("get-List-Checkbox", this.listReSelected)
          console.log(this.listReSelected);
    },

    /**
     * hàm hiện thông tin trên popup khi nhấn vào Sửa
     * Nguyễn Văn Cương 25/09/2022
     */
    rowDBClick(recordID) {
      //bấm dbclick để sửa
      this.detailFormMode = 2;
      this.$emit("custom-open-dbclick", recordID, this.detailFormMode);
    },

    /**
     * hàm lấy thông tin nhân viên khi xóa
     * Nguyễn Văn Cương 25/09/2022
     */
    getRecordDetele(recordID, recordCode){
        this.getrecorddetetevalue = recordID;
        this.getrecorddeteteCode = "<" + recordCode + ">";
        
    },
 
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.PopupNotificationMode = 2;
            this.idRecordDelete = this.getrecorddetetevalue; //lưu id employee cần xóa
        }else if(this.checkDelete == 1){
          //bấm nhân bản
           this.detailFormMode = 1;
           this.$emit("custom-open-dbclick", this.getrecorddetetevalue, this.detailFormMode);
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
       this.popupAskCance = true; //lưu trạng thái đóng popup hỏi người dùng
    },

    /**
     * hàm format thuế
     * Nguyễn Văn Cương 01/10/2022
     */
    formatTaxRecord(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = "Có giảm thuế";
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = "Không giảm thuế";
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = "Chưa xác định";
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
     * hàm format trạng thái
     * Nguyễn Văn Cương 01/10/2022
     */
    formatStatusRecord(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = "Đang sử dụng";
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = "Ngưng sử dụng";
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
    formatDateRecord(date) {
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
     * hàm format giới tính 
     * Nguyễn Văn Cương 01/10/2022
     */
    formatGenderRecord(gender){

      //giá trị 1 là nữ 
       if(gender == enums.FEMALE){
         return gender = "Nữ";
      //giá trị 2 là nam
       }else if(gender == enums.MALE){
         return gender = "Nam";
       //giá trị 0 là khác
       }else if (gender == enums.ELSE){
         return gender = "Khác";
      //không có cho thành rỗng
       }else{
         return gender = "";
       }
    },

    formatNatureRecord(value){
       //giá trị 1 là nữ 
       if(value == enums.Product){
         return value = "Hàng hóa";
      //giá trị 2 là nam
       }else if(value == enums.Service){
         return value = "Dịch vụ";
       //giá trị 0 là khác
       }else if (value == enums.Material){
         return value = "Nguyên vật liệu";
      //không có cho thành rỗng
       }else if (value == enums.FiProduct){
         return value = "Thành phẩm";
      //không có cho thành rỗng
       }else if (value == enums.Tools){
         return value = "Công cụ dụng cụ";
      //không có cho thành rỗng
       }else{
         return value = "";
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
    async deleteRecord() {
      try {
        this.isShowToast = false;
          //lấy employeeid đã lưu 
          var id = this.idRecordDelete; 
            //đóng popup hỏi người dùng
            this.ClosePopupAsk();
          var Incurred = true;
            if(this.CheckDeleteIns){
              await this.CheckDelete(id);
              if(this.IncurredID != null){
                Incurred = false;
                this.isShowAskDelete = true;
                this.PopupNotificationMode = 4;
              }
            }
            if(Incurred){
              await fetch(
            configs[this.baseURL] + id,
            { method: "DELETE" })
            .then((res) => res.json())
            .then((data) => {
              if (data.errorCode) {
              //mở popup thông báo
              this.ShowToast(this.ToastStatus = false);
              }
              else{
                  //load lại data
                this.$emit("data-load-delete");
                this.ShowToast(this.ToastStatus = true);
                console.log(data);
              }
            })
            .catch((res) => {
              this.ShowToast(this.ToastStatus = false);
              console.log(res);
            });  
            }
      } catch (error) {
        console.log(error);
      }
    },

    /**
    Hàm check phát sinh khi xóa
    Nguyễn Văn Cương 15/11/2022
     */
    async CheckDelete(recordid){
      try {
        this.IncurredID = null;
        await fetch(configs[this.baseURL] + "checkdelete?checkdelete=" + recordid, {method: "GET"})
        .then((res) => res.json())
        .then((data) => {
          if (data.errorCode) {
            //mở popup thông báo
            this.ShowToast(this.ToastStatus = false);
          }
          else{
            this.IncurredID = JSON.stringify(data);
          }
        })
      } catch (error) {
        console.log(error);
      }
    },
    
  },
  created() {

  },
  data() {
    return {
      //lưu dữ liệu nhân viên
      records: [], 
       //lưu nhân viên đã chọn
      reSelected: {},
      //lưu trạng thái mở popup
      detailFormMode: 1, 
       //gọi popup hỏi có xóa không
      isShowAskDelete: false,
      //nút hủy xóa
      popupAskCance: false, 
      //lưu id nhân viên cần xóa
      idRecordDelete:0, 
      //trạng thái xóa
      checkDelete: 2,  
       //lưu id nhân viên cần xóa
      getrecorddetetevalue: 0,
      //lưu code nhân viên cần xóa
      getrecorddetetecode: '', 
      //lưu trạng thái checkbox
      stateCheckAll: false, 
      //lưu danh sách nhân viên cần xóa
      listReSelected: [], 
      //hiển thị thông báo
      isShowToast: false, 
      //trang thái thông báo
      ToastStatus: true, 
      //nội dung thông báo
      ToastMess:{}, 
      //màu nội dung thông báo
      ToastMess_color: {}, 
      //css thông báo
      Toastcss:{}, 
       //icon thông báo
      Toastcssicon: {},
      //lưu vị trí của dropbox
      StyleDropbox:{}, 
      //thay đổi trạng thái icon mở td con
      Changeicon: false,
      //thay đổi trạng thái mở td con
      openChildMuti: false,
      //lưu phát sinh
      IncurredID: null,
      //kiểu mở popup thông báo
      PopupNotificationMode: 2,

    };
  },
};
</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}

.contentner{
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
.content-table table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: unset;
  font-size: 13px;

}

td, th {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 42px;
  font-size: 13px;

}
.content-table tr td,
.content-table tr th {
  padding-left: 8px;
  text-align: left;
  cursor: pointer;
}.content-table tbody tr:hover {
  background-color: rgba(80,184,60,0.1);
}.content-table tr:active{
  background-color: #E5F3FF;
}
.content-table table thead tr th:first-child{
  min-width: 30px;
}

.tab-th-select {
  text-align: center !important;
  padding: 0 !important;
  z-index: 1;
}
.tab-th-amount{
  text-align: right !important;
  z-index: 1;
}
.content-table thead {
  background-color: rgb(236, 238, 241);
}.content-table table thead tr th:last-child{
  min-width: 110px;

  background-color: rgb(236, 238, 241);
  border-left: 1px solid rgb(216, 211, 211);
}
.content-table table tbody tr td:last-child{
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
  position: sticky;
  right: 0; 
  border-left: 1px solid rgb(216, 211, 211);
  z-index: 1;
  background-color: #fff;

}
.content-table table tbody tr td:last-child:has(.mdrop-datatbl){
  z-index: 2;
}

.content-table table thead tr th:last-child{
  position: sticky;
  right: 0; 
  border-left: 1px solid rgb(216, 211, 211);
}
.tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 3px;
  
}.tab-th-select-lable{
   margin-top: 16px;
   margin-left: 30px;
   font-weight: 600;
}.btnopendrop{
  position: relative;
  width: 30px;
  height: 30px;
  margin-top: 12px;

}.content-table table thead tr th{
  font-family: Misa Fonts Bold;
  position: sticky;
  top: 0;
  background-color: rgb(236, 238, 241);
  z-index: 3;
  font-size: 12px;
  
}.tooltip{
  width: 140px;
  font-size: 11px;
  position: absolute;
  top: 30px;
  right: 0;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 5;
  text-align: center;
  color: #fff;
  visibility: hidden;
}
.tab:hover .tooltip{
  visibility: visible;
  opacity: 1;
  width: 313px;
  font-size: 10px;
  top: 45px;
  right: -70px;
  font-family: Misa Fonts Semibold;
  height: 15px;
}.tab-b:hover .tooltip{
  visibility: visible;
  opacity: 1;
  font-family: Misa Fonts Semibold;
  width: 150px;
  font-size: 10px;
  right: -5px;
  top: 45px;
  height: 15px;
}.cmnd:hover .tooltip{
  visibility: visible;
  opacity: 1;
}.content-table table tfoot{
  background-color: rgb(236, 238, 241);
  height: 30px;
  width: 100%;
  z-index: 4;
  position: sticky;
  bottom: 0;
}.content-table table tfoot td{
  height: 30px;
  font-family: Misa Fonts Bold;
  border: none;
}.mutitable{
  display: flex;
  align-items: center;
}
.mutitable-icon{
  background-image: var(--icon);
  background-position: -605px -305px;
  background-repeat: no-repeat;
  width: 30px;
  height: 30px;
}.mutitable-label{
  padding-left: 50px;
}.mutitable-icon.b{
  background-position: -556px -305px;
}.mutitable-label.b{
  padding-left: 28px;
}.mutitable-label.c{
  padding-left: 0;
}.filter-header-icon{
  background-image: var(--icon);
  background-position: -1688px -565px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  float: right;
  margin-right: 5px;
  visibility: hidden;
}.content-table table thead tr th:hover .filter-header-icon{
  visibility: visible;
}
</style>

