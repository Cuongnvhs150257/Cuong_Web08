export default{
    // phím ESC
    ESC: 27, 

    // phím CTRL
    CTRL: 17, 

    // phím SHIFT
    SHIFT: 16, 

    //phím ALT
    ALT: 18,

    // phím S
    S: 83,

    //phím C
    C: 67,

    // phím UP
    UP: 38, 

    // phím DOWN
    DOWN: 40, 

    // phím ENTER
    ENTER: 13, 

    // phím TAB
    TAB: 9, 

    //phím ESC
    F2: 113,
    
    //nam
    MALE: 2,

    //nũ
    FEMALE: 1,

    //khác
    ELSE: 0,

    //Đang hoạt động
    ACTIVE: 1,

    //Ngưng hoạt động
    UNACTIVE: 2,

    //Chưa xác định
    UNKNOW: 0,

    //lỗi mã trống
    EmployeeCodeNull: "Mã không được để trống",

    //lỗi tên trống
    EmployeeNameNull: "Tên không được để trống",

    //lỗi đơn vị trống
    UnitNull: "Đơn vị không được để trống",

    //lỗi trống dữ liệu
    NullValue: "Không được để trống dữ liệu",

    //lỗi sai định dạng số điện thoại
    InvaluePhoneNumber: "Số điện thoại không hợp lệ",

    //lỗi sai định dạng email
    InvalueEmail: "Email không hợp lệ",

    //regex kiểm tra email
    RegexEmail: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
    
    //regex kiểm tra số điện thoại
    RegexPhone: /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/,
}