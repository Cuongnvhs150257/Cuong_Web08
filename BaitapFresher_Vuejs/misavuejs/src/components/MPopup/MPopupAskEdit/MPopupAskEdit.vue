<template>
  <div class="cover-popup">
    <div class="popupask">
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
          <MButtonAgreeAsk @click="agreesave" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MButtonAgreeAsk from "./MButtonAgreeEdit.vue";

export default {
  props: {
    getEmployeeCode: String,
  },
  methods: {

    /**
     * hàm nhấn nút không lưu dữ liệu thay đổi
     * Nguyễn Văn Cương 05/10/2022
     */
    popupaskno() {
      this.$emit("popup-ask-no");
    },

    /**
     * hàm đóng popup hỏi khi dữ liệu thay đổi
     * Nguyễn Văn Cương 05/10/2022
     */
    popupaskcance(){
      this.$emit("popup-ask-cance");
    },

    /**
     * hàm đồng ý lưu dữ liệu nhân viên 
     * Nguyễn Văn Cương 05/10/2022
     */
    agreesave() {
      this.$emit("agree-save-click");
    },
    
  },
  components: { MButtonAgreeAsk },
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
  right: 100px;
}
</style>


