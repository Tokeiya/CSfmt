// SfmtNativeBench.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"


int main()
{

	uint32_t key[32];
	for (auto i = 0; i < 32; i++) key[i] = i + 42;

	
	auto sfmt = static_cast<sfmt_t*>(_aligned_malloc(sizeof(sfmt_t), 16));
	sfmt_init_by_array(sfmt, key, 32);


	std::cout << sfmt_get_idstring(sfmt) << std::endl;
	std::cout << sfmt_get_min_array_size32(sfmt) << std::endl;
	std::cout << sfmt_get_min_array_size64(sfmt) << std::endl;
	
	
	
	
}

// プログラムの実行: Ctrl + F5 または [デバッグ] > [デバッグなしで開始] メニュー
// プログラムのデバッグ: F5 または [デバッグ] > [デバッグの開始] メニュー

// 作業を開始するためのヒント: 
//    1. ソリューション エクスプローラー ウィンドウを使用してファイルを追加/管理します 
//   2. チーム エクスプローラー ウィンドウを使用してソース管理に接続します
//   3. 出力ウィンドウを使用して、ビルド出力とその他のメッセージを表示します
//   4. エラー一覧ウィンドウを使用してエラーを表示します
//   5. [プロジェクト] > [新しい項目の追加] と移動して新しいコード ファイルを作成するか、[プロジェクト] > [既存の項目の追加] と移動して既存のコード ファイルをプロジェクトに追加します
//   6. 後ほどこのプロジェクトを再び開く場合、[ファイル] > [開く] > [プロジェクト] と移動して .sln ファイルを選択します
