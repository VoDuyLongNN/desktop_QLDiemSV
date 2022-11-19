alter table GiaoVien
add constraint fk_giaovien_bomon
foreign key (MaBM) references BoMon(MaBM)

alter table DiemSV
add constraint fk_sinhvien_hocki
foreign key (MaHK) references HocKi(MaHK)

alter table DiemSV
add constraint fk_diemsinhvien_sinhvien
foreign key (MaSV) references SinhVien(MaSV)

alter table DiemSV
add constraint fk_diemsinhvien_bomon
foreign key (MaBM) references BoMon(MaBM)

alter table DiemSV
add constraint fk_diemsinhvien_giaovien
foreign key (MaGV) references GiaoVien(MaGV)

alter table DiemTBHocKi
add constraint fk_diemtbhk_sinhvien
foreign key (MaSV) references SinhVien(MaSV)

alter table DiemTBHocKi
add constraint fk_diemtbhk_hocki
foreign key (MaHK) references HocKi(MaHK)

alter table DiemTBNam
add constraint fk_diemtbn_sinhvien
foreign key (MaSV) references SinhVien(MaSV)

