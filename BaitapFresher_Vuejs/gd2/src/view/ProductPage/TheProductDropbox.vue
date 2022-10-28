<template>
  <div class="droptbl" :class="{'dropbox-input-green': !inValueChange, 'dropbox-input-green': isShowDropbox }"  >
    <input class="dropbox-input product" type="text" :disabled="disabled" :style="width" :maxlength="maxlength" v-model="this.RecordSle[label]" :tabindex="tab" />
    <div :class="{'dropbox-input add' : iconadd}" @click="openPopupEdit"></div>
    <button class="droptbtn" @click="btnDropClick"></button>
    <div class="dropbl-data" v-show="isShowDropbox" ref="combobox">
        <div class="drop-for" v-for="d in DropboxShow" :key="d[value]" :class="{select : d[value] == RecordSle[value] }" @click="selectedRecord(d)" >
          <div class="drop-itemtbl" >{{d[label]}}</div>
        </div>
    </div>
    
  </div>
</template>

<script>
import configs from "../../configs/index";
export default {
  mounted() {
    window.addEventListener('mouseup', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
    props:{
        DropboxItem: [],
        tab: Number,
        maxlength: Number,
        value: String,
        label: String,
        disabled: Boolean,
        iconadd: Boolean,
        width: String,
    },
    methods:{
        /**
         * hàm chọn đơn vị
         * Nguyễn Văn Cương 05/10/2022
         */
        selectedRecord(drop){
          this.RecordName = drop;
          this.isShowDropbox = !this.isShowDropbox;
          this.$emit("get-recordvalue", drop.value);
          this.inValueChange = this.inValueCombox;
          this.selectItem = false;
        },
        btnDropClick(){
           this.isShowDropbox = !this.isShowDropbox;
           if(this.DropboxItem){
              this.DropboxShow = this.DropboxItem
           }else{
             this.loadUnit();
           }
        },
        openPopupEdit(){
          this.$emit("open-popup-edit");
        },
       
        /**
         * hàm lấy toàn bộ đơn vị
         * Nguyễn Văn Cương 05/10/2022
         */
        loadUnit(){
            fetch(configs.baseURLUnit, {
            method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.DropboxShow = data;
          console.log(data)
        })
        .catch((res) => {
          console.log(res);
        });
        },


      clickEventInterrupt(event){
      const isClick = this.$refs.combobox.contains(event.target);
      if(!isClick){
         this.isShowDropbox = false;
      }
      
    },
    },

    computed:{
        RecordSle(){
          
          return this.RecordName;
        }
    },
    data(){
      
      var RecordName= {
          //hiện thị tên đơn vị của nhân viên khi mở popup sửa nhân viên
            lable: this.EmployeeUnit 
      };
      return{
        isShowDropbox: false,
        selectItem: true,
        inValue: true, //nổi bật đơn vị đã chọn
        inValueChange: true,
        RecordName,
        DropboxShow: [],

      }
    }
}
</script>

<style scoped>
:root{
    --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}.select .drop-itemtbl {
  background-color: #2ca01c !important;
  color: #fff !important;
}
.droptbl{
  height: 31px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  background-color: #fff;
  margin-top: 5px;
  border-radius: 4px;
  border: 1px solid #bbbbbb;
}

.droptbtn{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  background: #fff;
  z-index: 3;
}
.dropbox-input.product{
  width: 80%;
  height: 27px;
  float: left;
  height: 100%;
  border: none;
  outline: none;
  padding-left: 10px;
  border-radius: 4px;
}
.droptbtn{
    background-image: var(--icon);
    background-position: -552px -353px;
    background-repeat: no-repeat;
}.droptbtn:hover{
    border: none;
    outline: none;
    background-color: #dddd;
}.dropbox-input.add:hover{
    background-color: #dddd;
}
.drop-for{
    width: 100%;
}.dropbl-data{
    position: absolute;
    width: 100%;
    z-index: 6;
    border: 1px solid #bbbb;
    top: 32px;
    border-radius: 4px;
    overflow-y: scroll;
    max-height: 184px;
}.dropbox-input.add{
    position: absolute;
    right: 30px;
    background-image: var(--icon);
    background-position: -24px -305px;
    background-repeat: no-repeat;
    width: 30px;
    height: 28px;
    z-index: 5;
    border-right: 1px solid #bbbb;
    cursor: pointer;
}
.drop-itemtbl{
  height: 32px;
  width: 94%;
  display: flex;
  align-items: center;
  background-color: #fff;
  font-size: 13px;
  font-weight: lighter;
  color: #000;
  z-index: 100;
  padding-left:8px ;
}
.drop-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.drop-data-hide{
  display: none;
}.dropbox-input-green{
  border: 1px solid #50B83C !important;
}.dropbox-input-red{
  border: 1px solid #ff0000 !important;
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
  background: #7c869c;
}
</style>