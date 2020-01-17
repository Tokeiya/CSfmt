// SfmtNativeBench.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"


int main()
{
	auto str = "2";
	auto sfmt_a = static_cast<sfmt_t*>(_aligned_malloc(sizeof(sfmt_t), 16));
	auto sfmt_b = static_cast<sfmt_t*>(_aligned_malloc(sizeof(sfmt_t), 16));

	auto array_a = static_cast<uint32_t*>(_aligned_malloc(sizeof(uint32_t)*1024, 16));
	auto array_b = static_cast<uint32_t*>(_aligned_malloc(sizeof(uint32_t)*1024, 16));

	std::cout << sfmt_get_idstring(sfmt_b);

	
	sfmt_init_gen_rand(sfmt_a, 1234);
	sfmt_init_gen_rand(sfmt_b, 1234);

	SFMT_jump(sfmt_b, str);

	std::ofstream wtr("G:/jump2.txt");


	for(auto i=0;i<SFMT_N32;i++)
	{
		wtr << sfmt_b->state->u[i] << std::endl;
	}
	
	

	sfmt_fill_array32(sfmt_a, array_a, 1024);

	for (auto i=0;i<20;i++)
	{
		std::cout << array_a[i] << std::endl;
	}

	std::cout << std::endl << std::endl;

	sfmt_fill_array32(sfmt_b, array_b, 1024);

	for (auto i = 0; i < 20; i++)
	{
		std::cout << array_b[i] << std::endl;
	}

	
	
	

	
	
	
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
