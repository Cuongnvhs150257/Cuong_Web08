<template>
  <div class="cover-popup" >
    <div class="popupnoti" v-if="MPopupN == 1"> 
      <div class="popupask-top">
        <div :class="classcssicon"></div>
        <div :class="classlabel">
          <label class="label-ask">{{ errors }}</label>
        </div>
      </div>
      <div class="popupask-bottom">
        <div class="popupask-bottom-btn" @click="closeNotification">
          <MButton
            :ButtonCss="'btn-button-openPopup-Employee'"
            :text="'Đóng'"
          />
        </div>
      </div>
    </div>

    <div class="popupask" v-if="MPopupN == 2">
      <div class="popupask-top">
        <div class="popupask-icon icon-ask"></div>
        <div class="popupask-icon-label-ask">
          <label class="label-ask"
            >Bạn có thực sự muốn xóa Nhân viên
            {{ getEmployeeCode }} không?</label
          >
        </div>
      </div>
      <div class="popupask-bottom-ask">
        <button class="popupask-bottom-right" @click="popupaskcance">
          Không
        </button>
        <div class="popupask-bottom-btn-ask"  @click="agreedelete">
            <MButton :ButtonCss="'btn-button-openPopup-Employee'" :text="'Có'" />
        </div>
        
      </div>
    </div>

     <div class="popupask" v-if="MPopupN == 3">
      <div class="popupask-top">
        <div class="popupask-icon icon-ask-edit"></div>
        <div class="popupask-icon-label-ask-edit">
          <label class="label-ask-edit"
            >Dữ liệu đã bị thay đổi. Bạn có muốn cất không?</label
          >
        </div>
      </div>
      <div class="popupask-bottom-edit">
        <button class="popupask-bottom-right-edit" @click="popupaskcance">Hủy</button>
        <div class="popupask-bottom-left">
          <button class="popupask-bottom-left-edit" @click="popupaskno">Không</button>
          <div class="popupask-bottom-left-btn" @click="agreesave">
            <MButton :ButtonCss="'btn-button-openPopup-Employee'" :text="'Có'" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "../MButton/MButton.vue";

export default {
  props:{
      errors: String,
      classcssicon: String,
      classlabel: String,
      getEmployeeCode: String,
      MPopupN: Number
  },
  methods: {
    /**
     * hàm đồng ý xóa nhân viên
     * Nguyễn Văn Cương 01/10/2022
     */
    closeNotification() {
      this.$emit("close-notification-click");
    },

    /**
     * hàm nhấn nút hủy xóa nhân viên 
     * Nguyễn Văn Cương 01/10/2022
     */
    popupaskcance() {
      this.$emit("popup-ask-cance");
    },

    /**
     * hàm đồng ý xóa nhân viên
     * Nguyễn Văn Cương 01/10/2022
     */
    agreedelete() {
      this.$emit("agree-delete-click");
    },

    /**
     * hàm nhấn nút không lưu dữ liệu thay đổi
     * Nguyễn Văn Cương 05/10/2022
     */
    popupaskno() {
      this.$emit("popup-ask-no");
    },
    /**
     * hàm đồng ý lưu dữ liệu nhân viên 
     * Nguyễn Văn Cương 05/10/2022
     */
    agreesave() {
      this.$emit("agree-save-click");
    },
  },
  components: { MButton },
};
</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.cover-popup {
  position: fixed;
  width: 100%;
  height: 100vh;
  right: 0;
  left: 0;
  top: 0;
  bottom: 0;
  background-color: rgba(12, 12, 12, 0.3);
  z-index: 1000;
}
.popupnoti {
  width: 450px;
  height: 180px;
  position: relative;
  top: 40%;
  margin: 0 auto;
  background-color: #fff;
}
.popupask-top {
  width: 100%;
  height: 100px;
  display: flex;
  position: relative;
}
.popupask-icon {
  width: 50px;
  height: 49px;
  margin-left: 50px;
  margin-top: 50px;
  border-bottom: 1px solid #bbbb;
}
.icon-noti {
  background-image: var(--icon);
  background-position: -752px -462px;
  background-repeat: no-repeat;
}
.icon-noti-duli {
  background-image: var(--icon);
  background-position: -598px -463px;
  background-repeat: no-repeat;
}
.popupask-icon-label {
  width: 300px;
  margin-top: 50px;
  border-bottom: 1px solid #bbbb;
  padding-top: 10px;
}
.popupask-icon-label-duli {
  width: 300px;
  margin-top: 40px;
  border-bottom: 1px solid #bbbb;
  padding-top: 10px;
}
.label-ask {
  color: #000;
}
.popupask-icon2 {
  width: 50px;
  height: 50px;
  background-color: #fff;
  border: none;
}
.icon-cance {
  background-image: var(--icon);
  background-position: -128px -131px;
  background-repeat: no-repeat;
}
.popupask-bottom-btn {
  width: 100px;
  height: 30px;
  position: absolute;
  right: 40%;
  top: 65%;
}.popupask {
  width: 450px;
  height: 180px;
  position: relative;
  top: 40%;
  margin: 0 auto;
  background-color: #fff;
  border-radius: 4px;
}
.popupask-top {
  width: 100%;
  height: 100px;
  display: flex;
  position: relative;
}
.popupask-icon {
  width: 50px;
  height: 49px;
  margin-left: 43px;
  margin-top: 50px;
  border-bottom: 1px solid;
}
.icon-ask {
  background-image: var(--icon);
  background-position: -598px -463px;
  background-repeat: no-repeat;
}
.popupask-icon-label-ask {
  width: 320px;
  margin-top: 30px;
  border-bottom: 1px solid #bbb;
  padding-top: 10px;
  line-height: 25px;
}
.label-ask {
  line-height: 20px;
}
.popupask-icon2 {
  width: 50px;
  height: 50px;
  background-color: #fff;
  border: none;
}
.icon-cance {
  background-image: var(--icon);
  background-position: -128px -131px;
  background-repeat: no-repeat;
}
.popupask-bottom-right {
  height: 38px;
  width: 80px;
  border: 1px solid black;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 20px;
  margin-left: 43px;
  position: absolute;
  left: 0;
}.popupask-bottom-btn-ask{
  width: 100px;
  height: 30px;
  position: absolute;
  right: 30px;
  top: 65%;
}
.popupask {
  width: 450px;
  height: 180px;
  position: relative;
  top: 40%;
  margin: 0 auto;
  background-color: #fff;
  border-radius: 4px;
}
.popupask-top {
  width: 100%;
  height: 100px;
  display: flex;
  position: relative;
}
.popupask-icon {
  width: 50px;
  height: 49px;
  margin-left: 50px;
  margin-top: 50px;
  border-bottom: 1px solid #bbbb;
}
.icon-ask-edit {
  background-image: var(--icon);
  background-position: -832px -462px;
  background-repeat: no-repeat;
}
.popupask-icon-label-ask-edit {
  width: 320px;
  margin-top: 45px;
  border-bottom: 1px solid #bbbb;
  padding-top: 10px;
  line-height: 25px;
}
.label-ask-edit {
  color: #000;
  font-size: 13px;
}
.popupask-icon2 {
  width: 50px;
  height: 50px;
  background-color: #fff;
  border: none;
}
.icon-cance {
  background-image: var(--icon);
  background-position: -128px -131px;
  background-repeat: no-repeat;
}
.popupask-bottom-right-edit {
  height: 38px;
  width: 80px;
  border: 1px solid #bbbb;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 20px;
  margin-left: 50px;
  position: absolute;
  left: 0;
}.popupask-bottom-edit{
  display: flex;
}.popupask-bottom-left-edit {
  height: 38px;
  width: 80px;
  border: 1px solid #bbbb;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 20px;
  margin-left: 50px;
  position: absolute;
  right: 105px;
}.popupask-bottom-left-btn{
  width: 100px;
  height: 30px;
  position: absolute;
  right: 30px;
  top: 65%;
}
</style>


