// dSfmtNativeBench.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"

void generate_sample()
{
	auto dSfmt = static_cast<dsfmt_t*>(_aligned_malloc(sizeof(dsfmt_t), 16));
	dsfmt_init_gen_rand(dSfmt, 0);
	
	std::ofstream wtr("G:\\output.txt");
	wtr << std::setprecision(17);

	for (auto i=0;i<1024;i++)
	{
		double d = dsfmt_genrand_close1_open2(dSfmt);
		wtr << d << "," << std::endl;
	}

	wtr.close();

	wtr = std::ofstream("G:\\after_state.txt");
	auto ptr = &dSfmt->status->u[0];
	
	for(auto i=0;i<DSFMT_N64;i++)
	{
		wtr << ptr[i] << "," << std::endl;
	}

	wtr.close();

}

void main()
{
	auto dSfmt = static_cast<dsfmt_t*>(_aligned_malloc(sizeof(dsfmt_t), 16));
	dsfmt_init_gen_rand(dSfmt, 114514);

	std::ofstream wtr("g:\\initState.txt");
	auto ptr = &dSfmt->status->u[0];

	for(auto i=0;i<DSFMT_N64+2;i++)
	{
		wtr << ptr[i] << ',' << std::endl;
	}

	wtr.close;

	wtr = std::ofstream("G:\\output.txt");

	for(auto i=0;i<1024;i++)
	{
		wtr << dsfmt_genrand_uint32(dSfmt) << "," << std::endl;
	}

	wtr.close();

	wtr = std::ofstream("G:\\AfterState.txt");
	
	
	for(auto i=0;i<DSFMT_N64+2;i++)
	{
		wtr << ptr[i] << ',' << std::endl;
	}


	wtr.close();
	
	
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
